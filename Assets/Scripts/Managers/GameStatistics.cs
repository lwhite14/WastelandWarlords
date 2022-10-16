public static class GameStatistics
{
    public static int TurnNumber = 1;
    public static int EnemiesFelled = 0;
    public static int HexesTraversed = 0;

    public static void ResetFields()
    {
        TurnNumber = 1;
        EnemiesFelled = 0;
        HexesTraversed = 0; 
    }
}