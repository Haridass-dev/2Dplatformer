using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class controllplayer : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 vectore;
    public float speed = 10f;
    public float upfore = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void move(InputAction.CallbackContext context)
    {

        vectore.Set(context.ReadValue<float>(),0 );
       rb.velocity += vectore * speed;




    }

    public void jumping(InputAction.CallbackContext context)
    {
        rb.AddForce(Vector2.up * upfore);
    }


}