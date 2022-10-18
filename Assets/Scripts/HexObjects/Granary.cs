using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granary : SettlementBuilding
{
    public int growth = 5;

    override public void EndTurn()
    {
        settlement.growth += growth;
    }
}
