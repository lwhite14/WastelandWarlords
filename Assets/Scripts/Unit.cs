using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexPathfinding;

public class Unit : MonoBehaviour
{
    public int movementPoints = 3;
    public HexCell cellOn { get; private set; }

    GameObject movementMarker;

    void Awake()
    {
        movementMarker = Resources.Load<GameObject>("GameObjects/MovementMarker");
    }

    public void SetCell(HexCell newCell)
    {
        this.cellOn = newCell;
        this.cellOn.unit = this;
        transform.SetParent(newCell.topTarget);
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public void Select() 
    {
        MovementNode[] movementNodes = MovementFinder.DisplayMovement(movementPoints, cellOn);
        foreach (MovementNode node in movementNodes) 
        {
            if (node.coordinates != cellOn.coordinates)
            {
                GameObject tempMovementMarker = Instantiate<GameObject>(movementMarker);
                tempMovementMarker.transform.SetParent(HexGrid.instance.hexCells[node.coordinates.X, node.coordinates.Z].topTarget);
                tempMovementMarker.transform.localPosition = new Vector3(0, 0.05f, 0);
            }
        }
    }
}
