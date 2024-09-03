using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie : MonoBehaviour
{

    public enum enemyState { Standby, Patrol, PlayerDetected, Attacking }
    playermovment _player;
    SpriteRenderer _sprite;
    Animator _anim;
    public enemyState _state;
    public int _maxhealth = 100;
    public int _currenthealth;
    public bool _flipX;
    [Header("Patrol")]
    public BoxCollider2D _patrolControl;
    public float _patrolSpeed;
    [Header("Combat")]
    public float _combatRadius;
    public Vector2 _randomCombatRate;
    public Vector2Int _combatPower;
    public float _attackTime = 0.4f;
    [Header("SFX")]
    public AudioSource _deadFX;
    public AudioSource _hurtFX;
    public AudioSource _attackFX;
    public float _attackFXDelay;

    float _t;
    float _y;
    bool _flip;
    float _attackCooldown, _attackTimer;
    public GameObject coin;

    void Start()
    {
        _player = FindObjectOfType<playermovment>();
        _anim = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _currenthealth = _maxhealth;
        _y = transform.position.y;
        _attackCooldown = Random.Range(1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        DetectPlayer();
        StateManager();
    }

    public void TakeDamage(int damage)
    {
        _currenthealth -= damage;
        _anim.Play("hurt");
        if (_currenthealth <= 0)
        {
            Die();
            Instantiate(coin, transform.position, Quaternion.identity);
        }
        _hurtFX.Play();

    }

    void StateManager()
    {
        bool _insideRange = _patrolControl.bounds.Contains(_player.transform.position);
        if (_insideRange && _state != enemyState.PlayerDetected && _state != enemyState.Attacking) { _state = enemyState.PlayerDetected; }
        if (!_insideRange && _state == enemyState.PlayerDetected) { _state = enemyState.Patrol; }
    }

    void Movement()
    {
        float _speed = _patrolSpeed;
        bool _walk = true;

        if (_state == enemyState.Patrol)
        {
            if (!_flip) { if (transform.position.x >= _patrolControl.bounds.center.x + _patrolControl.bounds.extents.x) { _flip = true; } }
            else { if (transform.position.x <= _patrolControl.bounds.center.x - _patrolControl.bounds.extents.x) { _flip = false; } }
        }
        if (_state == enemyState.PlayerDetected || _state == enemyState.Attacking)
        {
            int _p = DetectPlayer();
            if (_p == 1) { _flip = false; }
            if (_p == -1) { _flip = true; }

            if (_p == 0 && _state != enemyState.Attacking)
            {
                _speed = 0f;
                _attackTimer += Time.deltaTime;
                if (_attackTimer > _attackCooldown) { StartCoroutine(_attack()); }
                _walk = false;
            }
        }
        if (_state == enemyState.Attacking) { _speed = 0f; _walk = false; }

        if (_flip) { _speed *= -1f; }

        transform.Translate(Vector2.right * _speed * Time.deltaTime);
        _sprite.flipX = _flipX ? !_flip : _flip;
        _anim.SetBool("Walk", _walk);
    }

    int DetectPlayer()
    {
        int _result = 0;
        if (Vector3.Distance(transform.position, _player.transform.position) < _combatRadius) { return 0; }

        Vector2 directionToTarget = _player.transform.position - transform.position;
        float dotProduct = Vector2.Dot(directionToTarget.normalized, Vector2.right);
        if (dotProduct > 0) { _result = 1; }
        if (dotProduct < 0) { _result = -1; }

        return _result;
    }

    IEnumerator _attack()
    {
        if (_player.GetComponent<PlayeCombat>()._isDead) { _attackCooldown = 1f; yield break; }
        if (_state == enemyState.Attacking) { yield break; }

        _state = enemyState.Attacking;
        _anim.Play("attack");
        _attackFX.PlayDelayed(_attackFXDelay);
        yield return new WaitForSeconds(_attackTime);
        if (Vector3.Distance(transform.position, _player.transform.position) < _combatRadius)
        {
            _player.GetComponent<PlayeCombat>().TakeDamage(Random.Range(_combatPower.x, _combatPower.y));
        }
        _attackCooldown = Random.Range(_randomCombatRate.x, _randomCombatRate.y);
        _attackTimer = 0f;
        _state = enemyState.PlayerDetected;
    }

    void Die()
    {
        _anim.Play("die");
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        _deadFX.Play();
    }

    void OnDrawGizmos()
    {
        Vector2 _a = new Vector3(_patrolControl.bounds.center.x - _patrolControl.bounds.extents.x, _y);
        Vector2 _b = new Vector3(_patrolControl.bounds.center.x + _patrolControl.bounds.extents.x, _y);
        _y = transform.position.y;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_a, _b);
        Gizmos.DrawWireSphere(_a, 0.1f);
        Gizmos.DrawWireSphere(_b, 0.1f);

        Gizmos.color = new Color(1f, 0f, 0f, 0.2f);
        Gizmos.DrawCube(this._patrolControl.bounds.center, this._patrolControl.bounds.extents * 2f);
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, _combatRadius);
        if (_state != enemyState.Patrol && _player != null)
        {
            Gizmos.DrawLine(transform.position, _player.transform.position);
        }
    }

    



}
