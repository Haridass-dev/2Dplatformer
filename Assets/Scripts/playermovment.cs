using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class playermovment : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    [SerializeField] float _speed = 1f;
    [SerializeField] float _jumpheaight = 1f;

    [HideInInspector] public SpriteRenderer _spriteRenderer;
    Animator _anim;
    [HideInInspector] public groundedcheck _ground;
    float _horizontalinput;

    [HideInInspector] public PlayeCombat _combat;
    public bool _controllable = true;
    bool _isJumping;

    [Header("SFX")]
    public AudioSource _jumpFX;
    public AudioSource _landFX;
    [Space]
    public GameObject _smokePoint;
    public GameObject _smoke;

    public coinmanager _cm;
    public PlayeCombat pc;

   


    void Start()
    {
        _combat = GetComponent<PlayeCombat>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _ground = GetComponent<groundedcheck>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_combat._isDead || !_controllable)
        {
            _anim.SetLayerWeight(1, 0f);
            _anim.SetBool("Run", false);
            _rigidbody.velocity = Vector3.zero;
            return;
        }
        if (_isJumping && _rigidbody.velocity.y < 0f && _ground.isGrounded)
        {
            _isJumping = false;
            _landFX.Play();

            GameObject _s = Instantiate(_smoke, _smokePoint.transform.position, _smokePoint.transform.rotation);
            Destroy(_s, 1f);
        }
        _horizontalinput = 0f;
        if (CnInputManager.GetButton("A")) { _horizontalinput = -1f; }
        if (CnInputManager.GetButton("D")) { _horizontalinput = 1f; }

        if (CnInputManager.GetButtonDown("Jump")) { jump(); }

        move();
        siting();
        playerflip();
        running();
        airup();
        groundcheck();
    }









    public void move()
    {
        Vector2 xy = new Vector2(_speed * Time.fixedDeltaTime * _horizontalinput, _rigidbody.velocity.y);
        _rigidbody.velocity = xy;
    }

    public void jump()
    {
        if (_ground.isGrounded)
        {
            _rigidbody.AddForce(Vector2.up * _jumpheaight, ForceMode2D.Impulse);
            _jumpFX.Play();
            _isJumping = true;
        }
    }



    public void siting()
    {
        if (Input.GetButton("sit"))
        {
            _anim.SetBool("sit", false);
            _horizontalinput = 0f;

        }
        else
        {
            _anim.SetBool("sit", true);
        }
    }

    public void playerflip()
    {
        if (Mathf.Abs(_horizontalinput) > 0.1f)
        {
            if (_horizontalinput < 0)
            {
                _spriteRenderer.flipX = true;
            }
            else
            {
                _spriteRenderer.flipX = false;

            }
        }
    }

    public void running()
    {
        if (Mathf.Abs(_horizontalinput) > 0.1f)
        {
            _anim.SetBool("Run", true);
        }
        else
        {
            _anim.SetBool("Run", false);
        }
    }


    public void groundcheck()
    {

        if (_ground.isGrounded == true)
        {
            _anim.SetLayerWeight(1, 0f);
        }
        else
        {
            _anim.SetLayerWeight(1, 1f);
        }
    }

    public void airup()
    {
        if (_rigidbody.velocity.y > 0)
        {
            _anim.SetBool("airup", true);
        }
        else
        {
            _anim.SetBool("airup", false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("coin"))
        {
            Destroy(collision.gameObject);
            _cm.UpdateCoin();   
        }

        if (collision.gameObject.CompareTag("heart"))
        {
            Destroy(collision.gameObject);
            pc.fulllife();

        }
    }
}



