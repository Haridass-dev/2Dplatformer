using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public Slider _slider;
    public Enemie _enemy;

    void Start()
    {

    }

    void Update()
    {
        _slider.value = (float)_enemy._currenthealth / _enemy._maxhealth;
    }
}
