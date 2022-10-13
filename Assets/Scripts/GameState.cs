using System.Collections.Generic;
using UnityEngine;

public static class GameState
{
    public static HexCell           CellSelected { get; set; }      = null;
    public static List<HexCell>     CellsMovement { get; set; }     = new List<HexCell>();
    public static Unit              UnitSelected { get; set; }      = null;
    public static List<Unit>        Units { get; set; }             = new List<Unit>();
    public static List<Enemy>       Enemies { get; set; }           = new List<Enemy>();
    public static List<Settlement>  Settlements { get; set; }       = new List<Settlement>();
}