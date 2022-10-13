using UnityEngine;
using UnityEngine.UI;

public static class ResourceFactory
{
    public static GameObject    MovementMarker          = Resources.Load<GameObject>("GameObjects/MovementMarker");
    public static GameObject    SelectionMarker         = Resources.Load<GameObject>("GameObjects/SelectionMarker");

    public static Unit          Unit                    = Resources.Load<Unit>("GameObjects/Unit");
    public static Enemy         Enemy                   = Resources.Load<Enemy>("GameObjects/Enemy");

    public static Settlement    Settlement              = Resources.Load<Settlement>("GameObjects/Settlements/Settlement");
    public static GameObject    L1GFX                   = Resources.Load<GameObject>("GameObjects/Settlements/L1GFX");
    public static GameObject    L2GFX                   = Resources.Load<GameObject>("GameObjects/Settlements/L2GFX");
    public static GameObject    L3GFX                   = Resources.Load<GameObject>("GameObjects/Settlements/L3GFX");

    public static HexCell       HexCell                 = Resources.Load<HexCell>("GameObjects/HexTypes/HexCell"); 
    public static HexCell       HexCellWater            = Resources.Load<HexCell>("GameObjects/HexTypes/Water"); 
    public static HexCell       HexCellWaterShallow     = Resources.Load<HexCell>("GameObjects/HexTypes/WaterShallow"); 
    public static HexCell       HexCellPlains           = Resources.Load<HexCell>("GameObjects/HexTypes/Plains"); 
    public static HexCell       HexCellForest           = Resources.Load<HexCell>("GameObjects/HexTypes/Forest"); 
    public static HexCell       HexCellImpactSite       = Resources.Load<HexCell>("GameObjects/HexTypes/ImpactSite");

    public static Sprite        ForestSprite            = Resources.Load<Sprite>("Textures/Forest");
    public static Sprite        PlainsSprite            = Resources.Load<Sprite>("Textures/Plains");
    public static Sprite        ImpactSiteSprite        = Resources.Load<Sprite>("Textures/ImpactSite");
    public static Sprite        WaterSprite             = Resources.Load<Sprite>("Textures/Water");
    public static Sprite        WaterShallowSprite      = Resources.Load<Sprite>("Textures/WaterShallow");
    public static Sprite        SettlementL1Sprite      = Resources.Load<Sprite>("Textures/SettlementL1");
    public static Sprite        NoSettlementSprite      = Resources.Load<Sprite>("Textures/NoSettlement");
    public static Sprite        UnitSprite              = Resources.Load<Sprite>("Textures/Unit");
    public static Sprite        NoUnitSprite            = Resources.Load<Sprite>("Textures/NoUnit");
}

