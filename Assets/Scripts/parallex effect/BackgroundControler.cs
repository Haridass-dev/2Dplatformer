using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControler : MonoBehaviour
{
    private float _offset , _length;
    public GameObject Cam;
    public float Parallexeffect;
    
    void Start()
    {
        _offset = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    void FixedUpdate()
    {
        float distance = Cam.transform.position.x * Parallexeffect;
        float movement = Cam.transform.position.x * (1 - Parallexeffect);
        transform.position = new Vector3(_offset + distance, transform.position.y, transform.position.z);
        
        if (movement > _offset + _length)
        {
            _offset += _length;
        }
        else if (movement < _offset - _length)
        {
            _offset -= _length;
        }
    }
}
