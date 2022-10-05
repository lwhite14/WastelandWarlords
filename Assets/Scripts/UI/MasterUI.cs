using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterUI : MonoBehaviour
{
    public static MasterUI instance = null;

    public TerrainPanel terrainPanel;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateTerrainPanel(HexCell selectedCell) 
    {
        terrainPanel.UpdateTerrainPanel(selectedCell);
    }
}
