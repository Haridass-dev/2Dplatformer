using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharaterControl : MonoBehaviour
{

    [SerializeField] float speed = 1f;
    private float horizontalinput;
    private bool verticalinput;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Vector2 xy;
    [SerializeField] Vector2 showVelocity;
    [SerializeField] float jump;


    
    void Start()
    {
      rb = GetComponentInChildren<Rigidbody2D>();  
      sr = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        movement();
        flipplayer();
        jumpplayer();
    }

    void movement()
    {
        horizontalinput = Input.GetAxis("Horizontal");
        xy.Set(horizontalinput, 0);

        rb.velocity = xy * speed * Time.deltaTime;
    }

    void flipplayer()
    {
        if (horizontalinput < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;

        }
    }

    void jumpplayer ()
    {

        verticalinput = Input.GetButton("Jump");

        print(verticalinput);

        if (verticalinput)
        {
            rb.AddForce(Vector2.up * jump);
            GetComponent<Animator>().SetTrigger("jumped");
        }

        GetComponent<Animator>().SetTrigger("grounded");
    }
  
}
