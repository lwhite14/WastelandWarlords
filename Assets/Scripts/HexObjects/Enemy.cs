using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexPathfinding;
using Unity.VisualScripting;

public class Enemy : MonoBehaviour
{
    public float health = 4f;
    public float damage = 2f;
    public float sightRange = 5.0f;
    public Animator anim;
    public Animator uiAnim;
    GameObject GFX;
    GameObject UI;
    AudioSource audioSource;

    public bool canBeSeen { get; private set; } = false;
    public bool firstSeen { get; private set; } = false;
    public HexCell cellOn { get; private set; }
    public string enemyName { get; set; }

    private void Awake()
    {
        GFX = transform.GetChild(0).gameObject;
        UI = transform.GetChild(1).gameObject;
        audioSource = GetComponent<AudioSource>();
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
                if (!firstSeen) 
                {
                    FogOfWar.Instance.CalculateVertexAlphas(transform.position, new Vector3(transform.position.x, transform.position.y + 200.0f, transform.position.z), sightRange);
                    firstSeen = true;
                }
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

    public void Select()
    {
        PlaySound(0);
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
        PlayDeathSound();
        GameStatistics.EnemiesFelled++;
        GameState.Enemies.Remove(this);
        this.cellOn.enemy = null;
        Destroy(gameObject);
    }

    int RandomSound()
    {
        System.Random random = new System.Random();
        return random.Next(0, 3);
    }

    void PlaySound(int type = 0)
    {
        audioSource.Stop();
        if (type == 0) { audioSource.clip = ResourceFactory.EnemySelect[RandomSound()]; }
        audioSource.Play();
    }

    void PlayDeathSound()
    {
        audioSource.Stop();
        GameObject tempObj = Instantiate<GameObject>(ResourceFactory.EnemyDeath);
        tempObj.transform.position = transform.position;
    }

    public IEnumerator Movement() 
    {
        Unit unitTopLeft = HexGrid.Instance.hexCells[cellOn.coordinates.X + MovementFinder.TopLeft.X, cellOn.coordinates.Z + MovementFinder.TopLeft.Z].unit;
        Unit unitTopRight = HexGrid.Instance.hexCells[cellOn.coordinates.X + MovementFinder.TopRight.X, cellOn.coordinates.Z + MovementFinder.TopRight.Z].unit;
        Unit unitBottomLeft = HexGrid.Instance.hexCells[cellOn.coordinates.X + MovementFinder.BottomLeft.X, cellOn.coordinates.Z + MovementFinder.BottomLeft.Z].unit;
        Unit unitBottomRight = HexGrid.Instance.hexCells[cellOn.coordinates.X + MovementFinder.BottomRight.X, cellOn.coordinates.Z + MovementFinder.BottomRight.Z].unit;
        Unit unitLeft = HexGrid.Instance.hexCells[cellOn.coordinates.X + MovementFinder.Left.X, cellOn.coordinates.Z + MovementFinder.Left.Z].unit;
        Unit unitRight = HexGrid.Instance.hexCells[cellOn.coordinates.X + MovementFinder.Right.X, cellOn.coordinates.Z + MovementFinder.Right.Z].unit;
        if (unitTopLeft != null) 
        {
            anim.Play("Attack");
            transform.LookAt(HexGrid.Instance.hexCells[unitTopLeft.cellOn.coordinates.X, unitTopLeft.cellOn.coordinates.Z].topTarget, Vector3.up);
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
            unitTopLeft.GiveDamage(damage);
        }
        else if (unitTopRight != null)
        {
            anim.Play("Attack");
            transform.LookAt(HexGrid.Instance.hexCells[unitTopRight.cellOn.coordinates.X, unitTopRight.cellOn.coordinates.Z].topTarget, Vector3.up);
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
            unitTopRight.GiveDamage(damage);
        }
        else if (unitBottomLeft != null)
        {
            anim.Play("Attack"); 
            transform.LookAt(HexGrid.Instance.hexCells[unitBottomLeft.cellOn.coordinates.X, unitBottomLeft.cellOn.coordinates.Z].topTarget, Vector3.up);
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
            unitBottomLeft.GiveDamage(damage);
        }
        else if (unitBottomRight != null)
        {
            anim.Play("Attack");
            transform.LookAt(HexGrid.Instance.hexCells[unitBottomRight.cellOn.coordinates.X, unitBottomRight.cellOn.coordinates.Z].topTarget, Vector3.up);
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
            unitBottomRight.GiveDamage(damage);
        }
        else if (unitLeft != null)
        {
            anim.Play("Attack");
            transform.LookAt(HexGrid.Instance.hexCells[unitLeft.cellOn.coordinates.X, unitLeft.cellOn.coordinates.Z].topTarget, Vector3.up);
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
            unitLeft.GiveDamage(damage);
        }
        else if (unitRight != null)
        {
            anim.Play("Attack");
            transform.LookAt(HexGrid.Instance.hexCells[unitRight.cellOn.coordinates.X, unitRight.cellOn.coordinates.Z].topTarget, Vector3.up);
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
            unitRight.GiveDamage(damage);
        }
        yield return null;
    }
}
