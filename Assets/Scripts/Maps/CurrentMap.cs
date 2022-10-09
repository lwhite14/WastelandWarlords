using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentMap : MonoBehaviour
{
    public static CurrentMap instance = null;

    public Map currentMap;

    Map[] allMaps =
    {
        new Map(@"Assets\Maps\EnglishChannelMap.yaml"),
        new Map(@"Assets\Maps\WalesMap.yaml"),
        new Map(@"Assets\Maps\IslandMap.yaml")
    };

    [Tooltip(   "1: EnglishChannel\n" +
                "2: Wales\n" +
                "3: Island"),
     Range(0, 2)]
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
