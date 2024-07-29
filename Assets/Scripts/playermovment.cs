using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovment : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpheaight = 1f;
    SpriteRenderer _spriteRenderer;
    Animator _anim;
    groundedcheck _ground;



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
        if (Input.GetButtonDown("Jump")&& _ground.isGrounded)
        {
             _rigidbody.AddForce(Vector2.up * jumpheaight, ForceMode2D.Impulse);
        }

        
    }

    private void FixedUpdate()
    {
        float horizontalinput = Input.GetAxis("Horizontal");
        if (Input.GetButton("sit"))
        {
            _anim.SetBool("sit", false);
            horizontalinput = 0f;

        }
        else
        {
            _anim.SetBool("sit", true);
        }
        Vector2 xy = new Vector2(speed * Time.fixedDeltaTime * horizontalinput, _rigidbody.velocity.y);
        _rigidbody.velocity = xy;
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

        //_anim.SetBool("Run", horizontalinput != 0);

         if (Mathf.Abs(horizontalinput) > 0.1f)
         {
             _anim.SetBool("Run", true);
         }
         else
         {
             _anim.SetBool("Run", false);
         }

         if (_ground.isGrounded == true)
        {
            _anim.SetLayerWeight(1, 0f);
        }
        else
        {
            _anim.SetLayerWeight(1, 1f);
        }

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
