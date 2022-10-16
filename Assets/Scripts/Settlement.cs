using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settlement : MonoBehaviour
{
    public static int growthForLevel2 = 40;
    public static int growthForLevel3 = 80;


    public HexCell cellOn { get; private set; }
    public string settlementName { get; set; }
    public int level { get; private set; }
    public int growth { get; set; } = 0;
    public int growthPerTurn { get; set; }
    public Transform gfxSlot;
    public float sightRange = 5.0f;

    void Start()
    {
        level = 1;
        growthPerTurn = 10;
        UpgradeLevel();
        FogOfWar.instance.CalculateVertexAlphas(transform.position, new Vector3(transform.position.x, transform.position.y + 200.0f, transform.position.z), sightRange);
    }

    public void SetCell(HexCell newCell)
    {
        if (this.cellOn != null)
        {
            this.cellOn.unit = null;
        }
        this.cellOn = newCell;
        this.cellOn.settlement = this;
        transform.SetParent(newCell.topTarget);
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public void Select() 
    {
        MasterUI.instance.UpdateSettlementPanel(this);
    }

    public void EndTurnGrowth() 
    {
        growth += growthPerTurn;
        if (ReachedNextLevel()) { UpgradeLevel(); }
    }

    public void UpgradeLevel() 
    {
        if (gfxSlot.childCount == 1) { Destroy(gfxSlot.GetChild(0).gameObject); }
        if (level == 1) { GameObject gfx = Instantiate<GameObject>(ResourceFactory.L1GFX); gfx.transform.SetParent(gfxSlot); gfx.transform.localPosition = new Vector3(0, 0, 0); }
        if (level == 2) { GameObject gfx = Instantiate<GameObject>(ResourceFactory.L2GFX); gfx.transform.SetParent(gfxSlot); gfx.transform.localPosition = new Vector3(0, 0, 0); }
        if (level == 3) { GameObject gfx = Instantiate<GameObject>(ResourceFactory.L3GFX); gfx.transform.SetParent(gfxSlot); gfx.transform.localPosition = new Vector3(0, 0, 0); }
    }

    bool ReachedNextLevel() 
    {
        if (level == 1) 
        {
            if (growth >= growthForLevel2) 
            {
                level = 2;
                growth -= growthForLevel2;
                return true;
            }
        }
        if (level == 2)
        {
            if (growth >= growthForLevel3) 
            {
                level = 3;
                growth -= growthForLevel3;
                return true;
            }
        }
        return false;
    }
}
