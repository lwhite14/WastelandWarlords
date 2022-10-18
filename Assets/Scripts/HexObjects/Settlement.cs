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
    public List<SettlementBuilding> buildings { get; set; } = new List<SettlementBuilding>();

    SettlementBuilding buildingToPlace = null;

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

    public void DisplayGranaryMarkers()
    {
        buildingToPlace = ResourceFactory.Granary;
        DisplayBuildingMarkers();
    }

    public void DisplayMarketMarkers() 
    {
        buildingToPlace = ResourceFactory.Market;
        DisplayBuildingMarkers();
    }

    public void DisplayBuildingMarkers() 
    {
        ClickMode.UnitMode = false;
        ClickMode.BuildingPlacementMode = true;
        GameState.CellsBuilding = new List<HexCell>();

        HexCell topLeft = HexGrid.Instance.hexCells[cellOn.coordinates.X + HexPathfinding.MovementFinder.TopLeft.X, cellOn.coordinates.Z + HexPathfinding.MovementFinder.TopLeft.Z];
        HexCell topRight = HexGrid.Instance.hexCells[cellOn.coordinates.X + HexPathfinding.MovementFinder.TopRight.X, cellOn.coordinates.Z + HexPathfinding.MovementFinder.TopRight.Z];
        HexCell bottomLeft = HexGrid.Instance.hexCells[cellOn.coordinates.X + HexPathfinding.MovementFinder.BottomLeft.X, cellOn.coordinates.Z + HexPathfinding.MovementFinder.BottomLeft.Z];
        HexCell bottomRight = HexGrid.Instance.hexCells[cellOn.coordinates.X + HexPathfinding.MovementFinder.BottomRight.X, cellOn.coordinates.Z + HexPathfinding.MovementFinder.BottomRight.Z];
        HexCell left = HexGrid.Instance.hexCells[cellOn.coordinates.X + HexPathfinding.MovementFinder.Left.X, cellOn.coordinates.Z + HexPathfinding.MovementFinder.Left.Z];
        HexCell right = HexGrid.Instance.hexCells[cellOn.coordinates.X + HexPathfinding.MovementFinder.Right.X, cellOn.coordinates.Z + HexPathfinding.MovementFinder.Right.Z];

        if (topLeft.settlementBuilding == null)
        {
            GameState.CellsBuilding.Add(topLeft);
            GameObject tempObj = Instantiate<GameObject>(ResourceFactory.BuildingMarker);
            tempObj.transform.SetParent(topLeft.topTarget.transform);
            tempObj.transform.localPosition = new Vector3(0, 0, 0);
        }
        if (topRight.settlementBuilding == null)
        {
            GameState.CellsBuilding.Add(topRight);
            GameObject tempObj = Instantiate<GameObject>(ResourceFactory.BuildingMarker);
            tempObj.transform.SetParent(topRight.topTarget.transform);
            tempObj.transform.localPosition = new Vector3(0, 0, 0);
        }
        if (bottomLeft.settlementBuilding == null)
        {
            GameState.CellsBuilding.Add(bottomLeft);
            GameObject tempObj = Instantiate<GameObject>(ResourceFactory.BuildingMarker);
            tempObj.transform.SetParent(bottomLeft.topTarget.transform);
            tempObj.transform.localPosition = new Vector3(0, 0, 0);
        }
        if (bottomRight.settlementBuilding == null)
        {
            GameState.CellsBuilding.Add(bottomRight);
            GameObject tempObj = Instantiate<GameObject>(ResourceFactory.BuildingMarker);
            tempObj.transform.SetParent(bottomRight.topTarget.transform);
            tempObj.transform.localPosition = new Vector3(0, 0, 0);
        }
        if (left.settlementBuilding == null)
        {
            GameState.CellsBuilding.Add(left);
            GameObject tempObj = Instantiate<GameObject>(ResourceFactory.BuildingMarker);
            tempObj.transform.SetParent(left.topTarget.transform);
            tempObj.transform.localPosition = new Vector3(0, 0, 0);
        }
        if (right.settlementBuilding == null)
        {
            GameState.CellsBuilding.Add(right);
            GameObject tempObj = Instantiate<GameObject>(ResourceFactory.BuildingMarker);
            tempObj.transform.SetParent(right.topTarget.transform);
            tempObj.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public void PlaceBuilding(HexCell cell) 
    {
        SettlementBuilding settlementBuilding = Instantiate<SettlementBuilding>(buildingToPlace);
        settlementBuilding.transform.SetParent(cell.topTarget);
        settlementBuilding.transform.localPosition = new Vector3(0, 0, 0);
        settlementBuilding.settlement = this;
        cell.settlementBuilding = settlementBuilding;
        buildings.Add(settlementBuilding);
    }
}
