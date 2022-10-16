using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    Text text;
    Animator anim;

    void Awake()
    {
        text = GetComponentInChildren<Text>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (text.text == "")
        {
            anim.SetBool("Appear", false);
        }
        else 
        {
            anim.SetBool("Appear", true);
        }
    }
}
