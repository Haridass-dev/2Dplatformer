using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharaterControl : MonoBehaviour
{

    [SerializeField] float speed = 1f;
    private float horizontalinput;
    private bool verticalinput;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Vector2 newPostion;
    [SerializeField] Animator animator;
    [SerializeField] Vector2 showVelocity;
    [SerializeField] float jump;
    [SerializeField] float gravity;
    [SerializeField] bool grounded;


    
    void Start()
    {
      rigidBody = GetComponentInChildren<Rigidbody2D>();  
      spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        movement();
        flipplayer();
        jumpplayer();
        grouded();
    }

    void movement()
    {
        horizontalinput = Input.GetAxis("Horizontal");
        newPostion.Set(horizontalinput, 0);

        rigidBody.velocity = newPostion * speed * Time.deltaTime;
        rigidBody.gravityScale = 1f;
    }

    void flipplayer()
    {
        if (horizontalinput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;

        }
    }

    void jumpplayer ()
    {

        verticalinput = Input.GetButton("Jump");

        //print(verticalinput);

        if (verticalinput)
        {
            rigidBody.AddForce(Vector2.up * jump);
            //GetComponent<Animator>().SetTrigger("jumped");
            // gravity value chage to 1
            rigidBody.gravityScale = 1f;
            grounded = false;
            
        }
        else
       
        {
            rigidBody.gravityScale = gravity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        grounded = true;
       

    }

    void grouded()
    {
        
        if (grounded == true)
        {
            rigidBody.gravityScale = 1f;
            animator.SetBool("jumped", true);
            
            
        }
        else
        {
            rigidBody.gravityScale = gravity;
            animator.SetBool("jumped", false);

        }
    
    }
    


       
    

    
        









}
