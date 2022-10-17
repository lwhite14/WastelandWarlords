using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentMap : MonoBehaviour
{
    public static CurrentMap instance = null;

    public Map currentMap;
    public string mapFileLocation;

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

        currentMap = new Map(mapFileLocation);
    }
}

// Map file locations are in the format 'Maps\TutorialMap.yaml'.
