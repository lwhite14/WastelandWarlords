using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class HexCell : MonoBehaviour
{
    public string type;
    public HexCoordinates coordinates;
    public int movementCost = 1; 
    float selectedHeight = 2.0f;
    public Transform topTarget { get; private set; }
    [HideInInspector]public Unit unit = null;

    void Awake()
    {
        topTarget = transform.Find("TopTarget").transform;
    }


    public void Select()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + selectedHeight, transform.position.z);
        MasterUI.instance.UpdateTerrainPanel(this);
        if (unit != null) 
        {
            unit.Select();
        }
    }

    public void Unselect() 
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - selectedHeight, transform.position.z);
    }
}
