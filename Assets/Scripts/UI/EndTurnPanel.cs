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
        yield return new WaitForSeconds(0.5f);
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
        if (GameState.CellSelected != null) { GameState.CellSelected.Select(); }
        MasterUI.instance.EndTurnPanel(false);
        GameStatistics.TurnNumber++;
        GameStatistics.Moolah += 1500;
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
