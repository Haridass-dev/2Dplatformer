using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class PlayeCombat : MonoBehaviour
{
    public Animator anim;
    public Transform hitbox;
    public LayerMask enemy;
    public float attackrange = 0.5f;
    public int attackdamage = 40;
    public float attackrate = 2f;
    public float nextattacktime = 0.5f;
    float _timer;
    [Header("Player Health")]
    [Range(0, 100)] public int Health = 3;
    public MultipleIconValueBarTool _healthBar;

    public AudioSource _swordFX;
    public AudioSource _hurtFX;

    Vector3 _hitboxPos;
    public bool _isDead;
    playermovment _p;

    void Start()
    {
        _p = GetComponent<playermovment>();
        anim = GetComponent<Animator>();
        Health = 100;
        _healthBar.SetNowValue(_healthBar.MaxTotalValue);
        _healthBar.RefreshUI();
        _hitboxPos = hitbox.localPosition;
    }


    void Update()
    {
        if (_isDead || !_p._controllable) { return; }
        if (CnInputManager.GetButtonDown("Fire1") && Time.time > _timer && _p._ground.isGrounded)
        {
            _timer = Time.time + nextattacktime;
            Attack();
        }

        hitbox.localPosition = _p._spriteRenderer.flipX ? new Vector3(-_hitboxPos.x, _hitboxPos.y, _hitboxPos.z) : _hitboxPos;

    }

    public void TakeDamage(int damage)
    {
        if (Health <= 0) { return; }
        Health -= damage;
        if (Health <= 0)
        {
            _isDead = true;
            anim.Play("dead");
        }
        else
        {
            anim.Play("hit");
        }
        FindAnyObjectByType<followplayer>().TriggerShake(0.1f);

        _healthBar.SetNowValue(_healthBar.MaxTotalValue * (Health / 100f));
        _healthBar.RefreshUI();

        _hurtFX.Play();
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(hitbox.position, attackrange, enemy);

        foreach (Collider2D enemy in hitenemies)
        {
            enemy.GetComponent<Enemie>().TakeDamage(attackdamage);
        }

        _swordFX.Play();

    }

    void OnDrawGizmos()
    {

        if (hitbox == null) { return; }
        Gizmos.DrawWireSphere(hitbox.position, attackrange);
    }
    public void fulllife()
    {
        Health = 100;
        _healthBar.SetNowValue(_healthBar.MaxTotalValue * (Health / 100f));
        _healthBar.RefreshUI();
    }
    

}
