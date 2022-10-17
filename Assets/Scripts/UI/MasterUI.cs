using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterUI : MonoBehaviour
{
    public static MasterUI instance = null;

    public TerrainPanel terrainPanel;
    public CharacterPanel characterPanel;
    public SettlementPanel settlementPanel;
    public TopDock topDock;

    public GameObject endTurnPanel;

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
        characterPanel.UpdateCharacterPanel(null, null);
        settlementPanel.UpdateSettlementPanel(null);
    }

    public void UpdateAllUI()
    {
        if (GameState.CellSelected != null)
        {
            UpdateTerrainPanel(GameState.CellSelected);
            UpdateUnitPanel(GameState.CellSelected.unit, GameState.CellSelected.enemy);
            UpdateSettlementPanel(GameState.CellSelected.settlement);
        }
        UpdateTopDock();
    }

    public void UpdateTerrainPanel(HexCell selectedCell)
    {
        terrainPanel.UpdateTerrainPanel(selectedCell);
    }

    public void UpdateUnitPanel(Unit selectedUnit, Enemy enemy)
    {
        characterPanel.UpdateCharacterPanel(selectedUnit, enemy);
    }

    public void UpdateSettlementPanel(Settlement selectedSettlement)
    {
        settlementPanel.UpdateSettlementPanel(selectedSettlement);
    }

    public void UpdateTopDock()
    {
        topDock.SetMoolah(GameStatistics.Moolah);
        topDock.SetTurn(GameStatistics.TurnNumber);
    }

    public void EndTurnPanel(bool isOn)
    {
        endTurnPanel.SetActive(isOn);
    }
}
