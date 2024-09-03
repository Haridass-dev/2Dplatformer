using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class dialogue : MonoBehaviour
{
    [Header("UIreferences")]
    [SerializeField] TMP_Text speakertext;
    [SerializeField] TMP_Text dialoguetext;
    [SerializeField] Image portraitimage;

    [Header("dialogue content")]
    [SerializeField] string[] speaker;
    [TextArea]
    [SerializeField] string[] dialoguewords;
    [SerializeField] Sprite[] portrait;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
