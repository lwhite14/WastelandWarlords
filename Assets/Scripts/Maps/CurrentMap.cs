using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentMap : MonoBehaviour
{
    public static CurrentMap instance = null;

    public Map currentMap;

    Map[] allMaps =
    {
        new Map(@"Maps\TutorialMap.yaml")
    };

    [Tooltip("0: Tutorial"),
     Range(0, 0)]
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
