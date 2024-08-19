using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class controlplayer : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform groundcheck;
    [SerializeField] LayerMask groundlayer;
    [SerializeField] float speed = 6f;
    [SerializeField] float JumpingPower = 16f;
    private bool isFacingRight = true;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
