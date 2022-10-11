using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettlementPanel : MonoBehaviour
{
    public Text text;
    public Image image;

    public void UpdateSettlementPanel(Settlement settlement)
    {
        if (settlement != null)
        {
            text.text = settlement.settlementName;
            image.sprite = ResourceFactory.SettlementL1Sprite;
        }
        else 
        {
            text.text = "No Settlement...";
            image.sprite = ResourceFactory.NoSettlementSprite;
        }
    }
}
