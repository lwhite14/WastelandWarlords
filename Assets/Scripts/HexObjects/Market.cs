using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : SettlementBuilding
{
    public int moolah = 200;

    override public void EndTurn()
    {
        GameStatistics.Moolah += moolah;
    }
}
