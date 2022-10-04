using UnityEngine;

public static class HexTypes
{
    public static HexCell GetDefault() { return Resources.Load<HexCell>("GameObjects/HexCell"); ; }
    public static HexCell GetWater() { return Resources.Load<HexCell>("GameObjects/Water"); ; }
    public static HexCell GetShallowWater() { return Resources.Load<HexCell>("GameObjects/WaterShallow"); ; }
    public static HexCell GetPlains() { return Resources.Load<HexCell>("GameObjects/Plains"); ; }
    public static HexCell GetForest() { return Resources.Load<HexCell>("GameObjects/Forest"); ; }
    public static HexCell GetImpactSite() { return Resources.Load<HexCell>("GameObjects/ImpactSite"); ; }
}