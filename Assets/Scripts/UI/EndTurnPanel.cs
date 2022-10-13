using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnPanel : MonoBehaviour
{
    public void EndTurnButton()
    {
        EndTurn();
    }

    void EndTurn()
    {
        foreach (Unit unit in GameState.Units)
        {
            unit.ResetMovementPoints();
        }
        foreach (Settlement settlement in GameState.Settlements)
        {
            settlement.EndTurnGrowth();
        }
        MasterUI.instance.UpdateAllUI();
    }
}
