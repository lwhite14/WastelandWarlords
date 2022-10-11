using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class HexCell : MonoBehaviour
{
    public string type;
    public HexCoordinates coordinates;
    public int movementCost = 1; 
    public Transform topTarget { get; private set; }
    [HideInInspector]public Unit unit = null;
    [HideInInspector]public Settlement settlement = null;

    void Awake()
    {
        topTarget = transform.Find("TopTarget").transform;
    }


    public void Select()
    {
        GameObject selectionMarker = Instantiate<GameObject>(ResourceFactory.SelectionMarker);
        selectionMarker.transform.SetParent(topTarget);
        selectionMarker.transform.localPosition = new Vector3(0, 0, 0);
        MasterUI.instance.UpdateTerrainPanel(this);
        if (unit != null) 
        {
            unit.Select();
        }
    }

    public void Unselect() 
    {

    }
}
