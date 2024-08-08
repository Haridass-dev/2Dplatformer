using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeCombat : MonoBehaviour
{
    public Animator anim;
    public Transform hitbox;
    public LayerMask enemy;
    public float attackrange = 0.5f;
    public int attackdamage = 40;
    public float attackrate = 2f;
    float nextattacktime = 0.5f;



   
    void Start()
    {
       anim = GetComponent<Animator>();
    }

    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.X))
        {
            Attack();
        }
        
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
       Collider2D [] hitenemies = Physics2D.OverlapCircleAll(hitbox.position, attackrange, enemy);

        foreach(Collider2D enemy in hitenemies)
        {
            enemy.GetComponent<Enemie>().TakeDamage(attackdamage);
        }

       
    }

     void OnDrawGizmosSelected()
    {

        if (hitbox== null) {  return; }
        Gizmos.DrawWireSphere(hitbox.position, attackrange);
    }

}
