using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterControl : MonoBehaviour
{

    [SerializeField] float speed = 1f;
    [SerializeField] float horizontalinput;
    [SerializeField] Rigidbody2D rb;
    Vector2 xy;

    
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();  
    }

    
    void Update()
    {
        horizontalinput = Input.GetAxis("Horizontal");
        xy.Set(horizontalinput, 0);

        rb.velocity = xy * speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        
    }
}
