using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexPathfinding;

public class Enemy : MonoBehaviour
{
    public float health = 4f;
    public float damage = 2f;
    public Animator anim;
    public Animator uiAnim;
    GameObject GFX;
    GameObject UI;
    bool canBeSeen = false;

    public HexCell cellOn { get; private set; }
    public string enemyName { get; set; }

    private void Awake()
    {
        GFX = transform.GetChild(0).gameObject;
        UI = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if (canBeSeen)
        {
            GFX.SetActive(true);
            UI.SetActive(true);
        }
        else
        {
            GFX.SetActive(false);
            UI.SetActive(false);
        }

        bool hasBeenTrued = false;
        foreach (Unit unit in GameState.Units)
        {
            if (Vector3.Distance(unit.transform.position, transform.position) <= unit.sightRange)
            {
                canBeSeen = true;
                hasBeenTrued = true;
            }
        }
        foreach (Settlement settlement in GameState.Settlements)
        {
            if (Vector3.Distance(settlement.transform.position, transform.position) <= settlement.sightRange)
            {
                canBeSeen = true;
                hasBeenTrued = true;
            }
        }

        if (!hasBeenTrued)
        {
            canBeSeen = false;
        }
    }

    public void SetCell(HexCell newCell)
    {
        if (this.cellOn != null)
        {
            this.cellOn.unit = null;
        }
        this.cellOn = newCell;
        this.cellOn.enemy = this;
        transform.SetParent(newCell.topTarget);
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
    }

    public void GiveDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameState.Enemies.Remove(this);
        this.cellOn.enemy = null;
        Destroy(gameObject);
    }
}
