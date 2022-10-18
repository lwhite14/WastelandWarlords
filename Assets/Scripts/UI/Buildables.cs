using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buildables : MonoBehaviour
{

    public void Granary() 
    {
        if (GameState.CellSelected.settlement != null) { GameState.CellSelected.settlement.BuildGranary(); }
    }

    public void Market() 
    {
        if (GameState.CellSelected.settlement != null) { GameState.CellSelected.settlement.BuildMarket(); }
    }
}
