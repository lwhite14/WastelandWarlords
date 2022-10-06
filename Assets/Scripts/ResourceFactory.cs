
using UnityEngine;

public static class ResourceFactory
{
    public static GameObject    MovementMarker          = Resources.Load<GameObject>("GameObjects/MovementMarker");
    public static Unit          Unit                    = Resources.Load<Unit>("GameObjects/Unit");
    public static HexCell       HexCell                 = Resources.Load<HexCell>("GameObjects/HexTypes/HexCell"); 
    public static HexCell       HexCellWater            = Resources.Load<HexCell>("GameObjects/HexTypes/Water"); 
    public static HexCell       HexCellWaterShallow     = Resources.Load<HexCell>("GameObjects/HexTypes/WaterShallow"); 
    public static HexCell       HexCellPlains           = Resources.Load<HexCell>("GameObjects/HexTypes/Plains"); 
    public static HexCell       HexCellForest           = Resources.Load<HexCell>("GameObjects/HexTypes/Forest"); 
    public static HexCell       HexCellImpactSite       = Resources.Load<HexCell>("GameObjects/HexTypes/ImpactSite"); 
}

