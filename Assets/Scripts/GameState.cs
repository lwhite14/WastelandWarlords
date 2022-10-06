using System.Collections.Generic;
using UnityEngine;

public static class GameState
{
    public static HexCell          CellSelected         = null;
    public static List<HexCell>    CellsMovement        = new List<HexCell>();

    public static Unit             UnitSelected         = null;
    public static List<Unit>       Units                = new List<Unit>();
}