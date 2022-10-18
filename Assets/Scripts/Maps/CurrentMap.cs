using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentMap : MonoBehaviour
{
    public static CurrentMap Instance = null;

    public Map currentMap;
    public string mapFileLocation;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        currentMap = new Map(mapFileLocation);
    }
}

// Map file locations are in the format 'Maps\TutorialMap.yaml'.
