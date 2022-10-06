using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexPathfinding;

public class Unit : MonoBehaviour
{
    public int fullMovementPoints = 3;
    int movementPoints;

    public HexCell cellOn { get; private set; }

    void Start()
    {
        movementPoints = fullMovementPoints;
    }

    public void SetCell(HexCell newCell)
    {
        this.cellOn = newCell;
        this.cellOn.unit = this;
        transform.SetParent(newCell.topTarget);
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public void Move(HexCell newCell)
    {
        SetCell(newCell);
        movementPoints = newCell.topTarget.GetComponentInChildren<MovementMarker>().movementNode.movementPointsLeft;
    }

    public void Select() 
    {
        GameState.UnitSelected = this;
        MovementNode[] movementNodes = MovementFinder.DisplayMovement(movementPoints, cellOn);
        foreach (MovementNode node in movementNodes) 
        {
            if (node.coordinates != cellOn.coordinates)
            {
                GameObject tempMovementMarker = Instantiate<GameObject>(ResourceFactory.MovementMarker);
                tempMovementMarker.transform.SetParent(HexGrid.instance.hexCells[node.coordinates.X, node.coordinates.Z].topTarget);
                GameState.CellsMovement.Add(HexGrid.instance.hexCells[node.coordinates.X, node.coordinates.Z]);
                tempMovementMarker.transform.localPosition = new Vector3(0, 0.05f, 0);
                tempMovementMarker.GetComponent<MovementMarker>().movementNode = node;
            }
        }
    }

    public void ResetMovementPoints() 
    {
        movementPoints = fullMovementPoints;
    }
}
