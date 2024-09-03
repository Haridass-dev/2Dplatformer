using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinmanager : MonoBehaviour
{
    int coincount = 0;
    [SerializeField] TMPro.TextMeshProUGUI cointext;

    public void UpdateCoin()
    {
        coincount++;
        cointext.text = coincount.ToString();
    }
}
