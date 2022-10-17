using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettlementPanel : MonoBehaviour
{
    public Text text;
    public Image image;
    Animator anim;

    void Awake() 
    {
        anim = GetComponent<Animator>();    
    }

    public void UpdateSettlementPanel(Settlement settlement)
    {
        if (settlement != null)
        {
            anim.SetBool("Appear", true);
            text.text = settlement.settlementName + "\n" + 
                        "Level = " + settlement.level + "\n" + 
                        "Growth = " + settlement.growth;
            image.sprite = ResourceFactory.SettlementL1Sprite;
        }
        else 
        {
            anim.SetBool("Appear", false);
            text.text = "No Settlement...";
            image.sprite = ResourceFactory.NoSettlementSprite;
        }
    }
}
