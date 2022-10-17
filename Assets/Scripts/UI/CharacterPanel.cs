using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    public Text text;
    public Image image;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void UpdateCharacterPanel(Unit unit, Enemy enemy)
    {
        if (unit != null || enemy != null)
        {
            anim.SetBool("Appear", true);
            if (unit != null)
            {
                text.text = unit.unitName + ", health = " + unit.health;
                image.sprite = ResourceFactory.UnitSprite;
            }
            if (enemy != null)
            {
                text.text = enemy.enemyName + ", health = " + enemy.health;
                image.sprite = ResourceFactory.UnitSprite;
            }
        }
        else
        {
            anim.SetBool("Appear", false);
            text.text = "No Character...";
            image.sprite = ResourceFactory.NoUnitSprite;
        }
    }
}
