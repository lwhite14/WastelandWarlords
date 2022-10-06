using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnPanel : MonoBehaviour
{
    public void EndTurnButton() 
    {
        ResetMovementPoints();
    }

    void EndTurn() 
    {
        //End turn logic
    }

    void ResetMovementPoints()
    {
        foreach (Unit unit in FindObjectsOfType<Unit>()) 
        {
            unit.ResetMovementPoints();
        }
    }
}
