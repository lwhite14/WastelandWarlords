using System.Collections.Generic;
using UnityEngine;

public static class GameState
{
    public static HexCell           CellSelected { get; set; }      = null;
    public static List<HexCell>     CellsMovement { get; set; }     = new List<HexCell>();
    public static List<HexCell>     CellsAttack { get; set; }       = new List<HexCell>();
    public static List<HexCell>     CellsBuilding { get; set; }     = new List<HexCell>();
    public static Unit              UnitSelected { get; set; }      = null;
    public static List<Unit>        Units { get; set; }             = new List<Unit>();
    public static List<Enemy>       Enemies { get; set; }           = new List<Enemy>();
    public static List<Settlement>  Settlements { get; set; }       = new List<Settlement>();
    public static List<Collectable> Collectables { get; set; }      = new List<Collectable>();

    public static void ResetFields() 
    {
        CellSelected = null;
        CellsMovement = new List<HexCell>();
        CellsAttack = new List<HexCell>();
        CellsBuilding = new List<HexCell>();
        UnitSelected = null;
        Units = new List<Unit>();
        Enemies = new List<Enemy>();
        Settlements = new List<Settlement>();
        Collectables = new List<Collectable>();
    }
}