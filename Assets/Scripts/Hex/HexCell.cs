using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour
{
    public string type;
    public HexCoordinates coordinates;
    public bool selected = false;
    float selectedHeight = 2.0f;

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
