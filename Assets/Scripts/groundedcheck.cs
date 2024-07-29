using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundedcheck : MonoBehaviour
{
    public Transform groundCheck; // A point on the character to check if grounded.
    public float checkRadius = 0.2f; // Radius of the circle to check for ground.
    public LayerMask groundLayer; // Layer mask to specify what is ground.

   public bool isGrounded;

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }

    void OnDrawGizmos()
    {
        if (groundCheck == null)
            return;

        Gizmos.color = isGrounded ? Color.red : Color.green ;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
