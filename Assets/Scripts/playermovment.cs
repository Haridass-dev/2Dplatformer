using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using CnControls;

public class playermovment : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    [SerializeField] float _speed = 1f;
    [SerializeField] float _jumpheaight = 1f;

    SpriteRenderer _spriteRenderer;
    Animator _anim;
    groundedcheck _ground;
    float _horizontalinput;



    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _ground = GetComponent<groundedcheck>();
    }

    // Update is called once per frame
    void Update()
    {
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














}



