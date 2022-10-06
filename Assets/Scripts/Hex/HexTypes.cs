using UnityEngine;

public static class HexTypes
{
    public static HexCell GetDefault() { return Resources.Load<HexCell>("GameObjects/HexTypes/HexCell"); }
    public static HexCell GetWater() { return Resources.Load<HexCell>("GameObjects/HexTypes/Water"); }
    public static HexCell GetShallowWater() { return Resources.Load<HexCell>("GameObjects/HexTypes/WaterShallow"); }
    public static HexCell GetPlains() { return Resources.Load<HexCell>("GameObjects/HexTypes/Plains"); }
    public static HexCell GetForest() { return Resources.Load<HexCell>("GameObjects/HexTypes/Forest"); }
    public static HexCell GetImpactSite() { return Resources.Load<HexCell>("GameObjects/HexTypes/ImpactSite"); }
}