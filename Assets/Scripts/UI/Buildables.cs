using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buildables : MonoBehaviour
{

    public void Granary() 
    {
        if (GameState.CellSelected.settlement != null) { GameState.CellSelected.settlement.DisplayGranaryMarkers(); }
    }

    public void Market() 
    {
        if (GameState.CellSelected.settlement != null) { GameState.CellSelected.settlement.DisplayMarketMarkers(); }
    }
}
