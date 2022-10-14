using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnPanel : MonoBehaviour
{
    public void EndTurnButton()
    {
        StartCoroutine(EndTurn());
    }

    IEnumerator EndTurn()
    {
        MasterUI.instance.EndTurnPanel(true);
        HexGrid.DestroyMarkers();
        HexGrid.ResetMarkerCellLists();
        yield return StartCoroutine(EnemyMoves());
        foreach (Unit unit in GameState.Units)
        {
            unit.ResetMovementPoints();
            yield return null;
        }
        foreach (Settlement settlement in GameState.Settlements)
        {
            settlement.EndTurnGrowth();
            yield return null;
        }
        MasterUI.instance.UpdateAllUI();
        GameState.CellSelected.Select();
        MasterUI.instance.EndTurnPanel(false);
        yield return null;
    }

    IEnumerator EnemyMoves() 
    {
        foreach (Enemy enemy in GameState.Enemies)
        {
            yield return StartCoroutine(enemy.Movement());
        }
        yield return null;
    }
}
