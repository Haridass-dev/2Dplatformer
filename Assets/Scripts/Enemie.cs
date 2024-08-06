using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie : MonoBehaviour
{

    

    [SerializeField] int _maxhealth = 100;
    [SerializeField] Animator _anim;
    int _currenthealth;
    
    void Start()
    {
        _currenthealth = _maxhealth; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
      _currenthealth -= damage;
        _anim.SetTrigger("hurt");

        if (_currenthealth < 0)
        {
            Die();
           
        }
    }

    void Die()
    {
        _anim.SetBool("isDead",true);
        GetComponent<Collider2D>().enabled =false;
        this.enabled = false;
       
       
    }

   

}
