using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    public Text text;
    public Image image;

    public void UpdateCharacterPanel(Unit unit, Enemy enemy)
    {
        if (unit != null)
        {
            text.text = unit.unitName;
            image.sprite = ResourceFactory.UnitSprite;
        }
        else
        {
            text.text = "No Character...";
            image.sprite = ResourceFactory.NoUnitSprite;
        }

        if (enemy != null)
        {
            text.text = enemy.enemyName + ", health = " + enemy.health;
            image.sprite = ResourceFactory.UnitSprite;
        }
        else
        {
            text.text = "No Character...";
            image.sprite = ResourceFactory.NoUnitSprite;
        }
    }
}
