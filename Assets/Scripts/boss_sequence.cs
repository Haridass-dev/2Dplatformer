using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_sequence : MonoBehaviour
{

    public GameObject _letterBox;
    public GameObject _buttons;
    public GameObject _enemy;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ResetToDefault()
    {
        FindAnyObjectByType<followplayer>()._state = followplayer.cameraState.PlayerFollow;
        _letterBox.SetActive(false);
        _buttons.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.CompareTag("Player"))
        {
            _letterBox.SetActive(true);
            _enemy.SetActive(true);
            _buttons.SetActive(false);
            FindAnyObjectByType<followplayer>()._state = followplayer.cameraState.EnemyFocus;
            Invoke("ResetToDefault", 2f);
        }
    }
}
