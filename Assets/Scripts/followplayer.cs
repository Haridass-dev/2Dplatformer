using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followplayer : MonoBehaviour
{
    public Vector3 offset;
    public Transform target;
    public Transform boss;
    public float smooth;
    public float shakeMagnitude = 0.5f;
    public float shakeFrequency = 0.5f;
    private float shakeTimeRemaining;

    public cameraState _state;
    public enum cameraState { PlayerFollow, EnemyFocus, EscapeRoute }

    float _sTime;
    Vector3 _sPower;

    void LateUpdate()
    {
        Vector3 vector3 = new Vector3(target.position.x, RestrictCameraMovement(target.position.y), target.position.z);
        //transform.position = target.position + offset;

        if (_state == cameraState.PlayerFollow)
        {
            transform.position = Vector3.Lerp(transform.position, vector3 + offset, smooth * Time.deltaTime);
            GetComponent<Camera>().orthographicSize = 5f;
        }
        if (_state == cameraState.EnemyFocus)
        {
            //if (Time.time > _sTime) { _sPower = Random.insideUnitSphere * shakeMagnitude; _sTime = Time.time + shakeFrequency; }
            transform.position = new Vector3(boss.transform.position.x, -21.4f, -10);// + _sPower;
            GetComponent<Camera>().orthographicSize = 4f;
        }
        if (_state == cameraState.EscapeRoute)
        {
            transform.position = new Vector3(12.31f, -12.86f, -10);
            GetComponent<Camera>().orthographicSize = 4f;
        }

        if (shakeTimeRemaining > 0)
        {
            transform.position = transform.position + Random.insideUnitSphere * shakeMagnitude;
            shakeTimeRemaining -= Time.deltaTime;
        }
    }

    public void TriggerShake(float duration)
    {
        shakeTimeRemaining = duration;
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
