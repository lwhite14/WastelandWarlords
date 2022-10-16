using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public static class ResourceFactory
{
    public static GameObject    MovementMarker          = Resources.Load<GameObject>("GameObjects/Markers/MovementMarker");
    public static GameObject    SelectionMarker         = Resources.Load<GameObject>("GameObjects/Markers/SelectionMarker");
    public static GameObject    AttackMarker            = Resources.Load<GameObject>("GameObjects/Markers/AttackMarker");

    public static Unit          Unit                    = Resources.Load<Unit>("GameObjects/Unit");
    public static Enemy         Enemy                   = Resources.Load<Enemy>("GameObjects/Enemy");
    public static Collectable   Battery                 = Resources.Load<Collectable>("GameObjects/Battery");

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


    public static List<AudioClip>   Command             = new List<AudioClip>() { Resources.Load<AudioClip>("Sounds/Command1"), Resources.Load<AudioClip>("Sounds/Command2"), Resources.Load<AudioClip>("Sounds/Command3") };
    public static List<AudioClip>   Select              = new List<AudioClip>() { Resources.Load<AudioClip>("Sounds/Select1"), Resources.Load<AudioClip>("Sounds/Select2"), Resources.Load<AudioClip>("Sounds/Select3") };
    public static List<AudioClip>   EnemySelect         = new List<AudioClip>() { Resources.Load<AudioClip>("Sounds/EnemySelect1"), Resources.Load<AudioClip>("Sounds/EnemySelect2"), Resources.Load<AudioClip>("Sounds/EnemySelect3") };
    public static GameObject        UnitDeath           = Resources.Load<GameObject>("Sounds/UnitDeath");
    public static GameObject        EnemyDeath          = Resources.Load<GameObject>("Sounds/EnemyDeath");
    public static GameObject        Punch               = Resources.Load<GameObject>("Sounds/Punch");
}

