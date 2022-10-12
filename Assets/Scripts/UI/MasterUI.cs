using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterUI : MonoBehaviour
{
    public static MasterUI instance = null;

    public TerrainPanel terrainPanel;
    public UnitPanel unitPanel;
    public SettlementPanel settlementPanel;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        unitPanel.UpdateUnitPanel(null);
        settlementPanel.UpdateSettlementPanel(null);
    }

    public void UpdateAllUI() 
    {
        UpdateTerrainPanel(GameState.CellSelected);
        UpdateUnitPanel(GameState.CellSelected.unit);
        UpdateSettlementPanel(GameState.CellSelected.settlement);
    }

    public void UpdateTerrainPanel(HexCell selectedCell) 
    {
        terrainPanel.UpdateTerrainPanel(selectedCell);
    }

    public void UpdateUnitPanel(Unit selectedUnit) 
    {
        unitPanel.UpdateUnitPanel(selectedUnit);
    }

    public void UpdateSettlementPanel(Settlement selectedSettlement) 
    {
        settlementPanel.UpdateSettlementPanel(selectedSettlement);
    }
}
