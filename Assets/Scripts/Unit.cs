using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public HexCell cellOn { get; private set; }


    public void SetCell(HexCell newCell)
    {
        this.cellOn = newCell;
        transform.SetParent(newCell.unitTarget);
        transform.localPosition = new Vector3(0, 0, 0);
    }
}
