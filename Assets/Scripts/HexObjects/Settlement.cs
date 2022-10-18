using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settlement : MonoBehaviour
{
    public static int GrowthForLevel2 = 40;
    public static int GrowthForLevel3 = 80;
    public static int BaseMoolah = 400;
    public static int BaseGrowthPerTurn = 10;

    public Transform gfxSlot;
    public float sightRange = 5.0f;

    public HexCell cellOn { get; private set; }
    public string settlementName { get; set; }
    public int level { get; private set; } = 1;
    public int growth { get; set; } = 0;

    void Start()
    {
        UpgradeLevel();
        FogOfWar.Instance.CalculateVertexAlphas(transform.position, new Vector3(transform.position.x, transform.position.y + 200.0f, transform.position.z), sightRange);
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

    }

    public void EndTurnGrowth() 
    {
        growth += BaseGrowthPerTurn;
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
            if (growth >= GrowthForLevel2) 
            {
                level = 2;
                growth -= GrowthForLevel2;
                return true;
            }
        }
        if (level == 2)
        {
            if (growth >= GrowthForLevel3) 
            {
                level = 3;
                growth -= GrowthForLevel3;
                return true;
            }
        }
        return false;
    }
}
