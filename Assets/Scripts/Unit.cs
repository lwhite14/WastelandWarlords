using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexPathfinding;

public class Unit : MonoBehaviour
{
    public int fullMovementPoints = 3;
    public float moveSpeed = 4.0f;
    int movementPoints;

    public HexCell cellOn { get; private set; }

    void Start()
    {
        movementPoints = fullMovementPoints;
    }

    public void SetCell(HexCell newCell)
    {
        if (this.cellOn != null)
        {
            this.cellOn.unit = null;
        }
        this.cellOn = newCell;
        this.cellOn.unit = this;
        transform.SetParent(newCell.topTarget);
    }

    public void Move(HexCell newCell)
    {
        this.cellOn.unit = null;
        this.cellOn = newCell;
        this.cellOn.unit = this;
        movementPoints = newCell.topTarget.GetComponentInChildren<MovementMarker>().movementNode.movementPointsLeft;

        MovementNode toNode = newCell.topTarget.GetChild(0).GetComponent<MovementMarker>().movementNode;
        StartCoroutine(LerpUnit(toNode, newCell));;
    }

    IEnumerator LerpUnit(MovementNode toNode, HexCell newCell) 
    {
        transform.SetParent(null);

        List<MovementNode> nodes = new List<MovementNode>();
        nodes.Add(toNode);

        bool cont = true;
        int currentNode = 0;
        while (cont) 
        {
            if (nodes[currentNode].prevNode != null)
            {
                nodes.Add(nodes[currentNode].prevNode);
                currentNode++;
            }
            else 
            {
                cont = false;
            }
            yield return null;
        }
        nodes.RemoveAt(nodes.Count - 1);
        for (int i = nodes.Count - 1; i >= 0; i--)
        {
            Vector3 startingPosition = transform.position;
            Vector3 endPosition = HexGrid.instance.hexCells[nodes[i].coordinates.X, nodes[i].coordinates.Z].topTarget.position;
            Vector3 newPosition;

            float progress = 0.0f;
            while (progress < 1.0f)
            {
                newPosition = Vector3.Lerp(startingPosition, endPosition, progress);
                transform.position = newPosition;
                progress += Time.deltaTime * moveSpeed;
                yield return null;
            }
            newPosition = Vector3.Lerp(startingPosition, endPosition, 1.0f);
            transform.position = newPosition;
            yield return null;
        }

        transform.SetParent(newCell.topTarget);
        transform.localPosition = new Vector3(0, 0, 0);
        yield return null;
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
