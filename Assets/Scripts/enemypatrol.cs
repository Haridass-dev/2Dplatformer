using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemypatrol : MonoBehaviour
{
    public int speed = 1;
    public Transform pointA, pointB;
    private Vector2 currenttarget;
    SpriteRenderer sr;
    public Vector2 checkRadius;
    public float angle = 4f;
    public LayerMask playerlayer;
    public bool isPlayerHere;
    public GameObject player;
    public float attackspeed = 1;
    Animator anim;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        isPlayerHere = Physics2D.OverlapBox(transform.position, checkRadius, angle, playerlayer);

        if (isPlayerHere )
        {
            anim.SetBool("isattacking",true);
            Vector3 playerPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, attackspeed* Time.deltaTime);
        }
        else
        {
            Patrol();
            anim.SetBool("isattacking", false);
        }
    }

    void Patrol()
    {
        if (transform.position == pointA.position)
        {
            currenttarget = pointB.position;
            sr.flipX = true;
        }
        else if (transform.position == pointB.position)
        {
            currenttarget = pointA.position;
            sr.flipX = false;
        }

        transform.position = Vector2.MoveTowards(transform.position, currenttarget, speed * Time.deltaTime);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = isPlayerHere ? Color.blue : Color.green;
        Gizmos.DrawCube(transform.position, checkRadius);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //GetComponent<Rigidbody2D>().AddForce(transform.forward * 1000f);
    }

}
