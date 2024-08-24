using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followplayer : MonoBehaviour
{
    public Vector3 offset;
    public Transform target;
    public float smooth;
    void LateUpdate()
    {
        Vector3 vector3 = new Vector3(target.position.x, RestrictCameraMovement(target.position.y), target.position.z);
        //transform.position = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, vector3 + offset, smooth * Time.deltaTime);
    }

    float RestrictCameraMovement(float targetY)
    {
        if (targetY < 0)
        {
            return targetY;
        }
        else
        {
            return 0;
        }
    }
}
