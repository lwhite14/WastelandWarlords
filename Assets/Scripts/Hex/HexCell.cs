using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class HexCell : MonoBehaviour
{
    public string type;
    public HexCoordinates coordinates;
    public int movementCost = 1; 
    public bool selected = false;
    float selectedHeight = 2.0f;
    public Transform unitTarget { get; private set; }

    void Awake()
    {
        unitTarget = transform.Find("UnitTarget").transform;
    }


    public void Select()
    {
        selected = true;
        transform.position = new Vector3(transform.position.x, transform.position.y + selectedHeight, transform.position.z);
        MasterUI.instance.UpdateTerrainPanel(this);
    }

    public void Unselect() 
    {
        selected = false;
        transform.position = new Vector3(transform.position.x, transform.position.y - selectedHeight, transform.position.z);
    }
}
