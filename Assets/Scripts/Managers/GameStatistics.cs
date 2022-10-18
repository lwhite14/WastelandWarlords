public static class GameStatistics
{
    static int turnNumber = 1;
    static int enemiesFelled = 0;
    static int hexesTraversed = 0;
    static int moolah = 0;

    public static int TurnNumber 
    {
        get { return turnNumber; }
        set { turnNumber = value; MasterUI.Instance.UpdateTopDock(); }
    }

    public static int EnemiesFelled 
    {
        get { return enemiesFelled; }
        set { enemiesFelled = value; }
    }

    public static int HexesTraversed 
    {
        get { return hexesTraversed; }
        set { hexesTraversed = value; }
    }

    public static int Moolah 
    {
        get { return moolah; }
        set { moolah = value; MasterUI.Instance.UpdateTopDock(); }
    }

    public static void ResetFields()
    {
        turnNumber = 1;
        enemiesFelled = 0;
        hexesTraversed = 0;
        moolah = 0;
    }
}