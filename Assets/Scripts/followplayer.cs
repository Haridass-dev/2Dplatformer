using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followplayer : MonoBehaviour
{
    public Vector3 offset;
    public Transform target;
    public float smooth;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, target.position + offset, smooth * Time.deltaTime);
  







    }
}
