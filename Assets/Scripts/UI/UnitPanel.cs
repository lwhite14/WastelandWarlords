using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitPanel : MonoBehaviour
{
    public Text text;
    public Image image;

    public void UpdateUnitPanel(Unit unit)
    {
        if (unit != null)
        {
            text.text = unit.unitName;
            image.sprite = ResourceFactory.UnitSprite;
        }
        else
        {
            text.text = "No Settlement...";
            image.sprite = ResourceFactory.NoUnitSprite;
        }
    }
}
