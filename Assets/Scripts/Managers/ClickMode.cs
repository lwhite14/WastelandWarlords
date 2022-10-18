

public static class ClickMode 
{
    static bool unitMode = true;
    static bool buildingPlacementMode = false;

    public static bool UnitMode 
    {
        get { return unitMode; }
        set { unitMode = value; }
    }
    public static bool BuildingPlacementMode 
    {
        get { return buildingPlacementMode; }
        set { buildingPlacementMode = value; }
    }

    public static void ResetFields() 
    {
        unitMode = true;
        buildingPlacementMode = false;
    }


}