using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentMap : MonoBehaviour
{
    public static CurrentMap instance = null;

    public Map currentMap;

    Map[] allMaps =
    {
        new Map(@"Maps\EnglishChannelMap.yaml"),
        new Map(@"Maps\WalesMap.yaml"),
        new Map(@"Maps\IslandMap.yaml"),
        new Map(@"Maps\GreeceMap.yaml"),
        new Map(@"Maps\SmallContinentsMap.yaml")
    };

    [Tooltip(   "0: EnglishChannel\n" +
                "1: Wales\n" +
                "2: Island\n" + 
                "3: Greece\n" + 
                "4: Small Continents"),
     Range(0, 4)]
    public int mapUsed = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        currentMap = allMaps[mapUsed];
    }
}
