using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementBuilding : MonoBehaviour
{
    public Settlement settlement { protected get; set; } = null;

    public GameObject GFX;

    virtual public void EndTurn() 
    {
        Debug.Log("Base class 'SettlementBuilding::EndTurn()' method.");
    }
}
