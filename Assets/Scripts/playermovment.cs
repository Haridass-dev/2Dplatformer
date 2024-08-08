using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class playermovment : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpheaight = 1f;

    SpriteRenderer _spriteRenderer;
    Animator _anim;
    groundedcheck _ground;
    float horizontalinput;



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
       

        siting();
        playerflip();
        running();
        airup();
        groundcheck();
    }

        

        
    




    public void move(InputAction.CallbackContext context)
    {
        horizontalinput = context.ReadValue<float>();
        Vector2 xy = new Vector2(speed * Time.fixedDeltaTime * horizontalinput, _rigidbody.velocity.y);
        _rigidbody.velocity = xy;
    }

    public void jump(InputAction.CallbackContext context)
    {
        if (_ground.isGrounded)
        {
            _rigidbody.AddForce(Vector2.up * jumpheaight, ForceMode2D.Impulse);
        }
    }



    public void siting()
    {
        if (Input.GetButton("sit"))
        {
            _anim.SetBool("sit", false);
            horizontalinput = 0f;

        }
        else
        {
            _anim.SetBool("sit", true);
        }
    }

    public void playerflip()
    {
        if (Mathf.Abs(horizontalinput) > 0.1f)
        {
            if (horizontalinput < 0)
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
        if (Mathf.Abs(horizontalinput) > 0.1f)
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

    

