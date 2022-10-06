using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace HexPathfinding
{
    public static class MovementFinder
    {
        static HexCoordinates topRight = new HexCoordinates(0, 1);
        static HexCoordinates topLeft = new HexCoordinates(-1, 1);
        static HexCoordinates bottomRight = new HexCoordinates(1, -1);
        static HexCoordinates bottomLeft = new HexCoordinates(0, -1);
        static HexCoordinates left = new HexCoordinates(-1, 0);
        static HexCoordinates right = new HexCoordinates(1, 0);


        public static MovementNode[] DisplayMovement(int unusedMovementPoints, HexCell startingCell)
        {
            List<MovementNode> returnCells = new List<MovementNode>();

            MovementNode initialNode = new MovementNode(null, unusedMovementPoints, startingCell.coordinates);
            CreateNodes(returnCells, initialNode);

            for (int i = 0; i < unusedMovementPoints; i++)
            {
                int iterations = returnCells.Count;
                for (int j = 0; j < iterations; j++)
                {
                    CreateNodes(returnCells, returnCells[j]);
                }
            }

            return returnCells.ToArray();
        }

        public static void CreateNodes(List<MovementNode> returnCells, MovementNode rootNode) 
        {
            if (rootNode.movementPointsLeft - HexGrid.instance.hexCells[(rootNode.coordinates + topRight).X, (rootNode.coordinates + topRight).Z].movementCost >= 0) 
            {
                MovementNode node = new MovementNode(rootNode, rootNode.movementPointsLeft, rootNode.coordinates + topRight);
                if (!returnCells.Contains(node)) 
                {
                    returnCells.Add(node);
                }
            } //top right
            if (rootNode.movementPointsLeft - HexGrid.instance.hexCells[(rootNode.coordinates + topLeft).X, (rootNode.coordinates + topLeft).Z].movementCost >= 0) 
            {
                MovementNode node = new MovementNode(rootNode, rootNode.movementPointsLeft, rootNode.coordinates + topLeft);
                if (!returnCells.Contains(node))
                {
                    returnCells.Add(node);
                }
            } //top left
            if (rootNode.movementPointsLeft - HexGrid.instance.hexCells[(rootNode.coordinates + bottomRight).X, (rootNode.coordinates + bottomRight).Z].movementCost >= 0)
            {
                MovementNode node = new MovementNode(rootNode, rootNode.movementPointsLeft, rootNode.coordinates + bottomRight);
                if (!returnCells.Contains(node))
                {
                    returnCells.Add(node);
                }
            } //bottom right
            if (rootNode.movementPointsLeft - HexGrid.instance.hexCells[(rootNode.coordinates + bottomLeft).X, (rootNode.coordinates + bottomLeft).Z].movementCost >= 0) 
            {
                MovementNode node = new MovementNode(rootNode, rootNode.movementPointsLeft, rootNode.coordinates + bottomLeft);
                if (!returnCells.Contains(node))
                {
                    returnCells.Add(node);
                }
            } //bottom left
            if (rootNode.movementPointsLeft - HexGrid.instance.hexCells[(rootNode.coordinates + left).X, (rootNode.coordinates + left).Z].movementCost >= 0) 
            {
                MovementNode node = new MovementNode(rootNode, rootNode.movementPointsLeft, rootNode.coordinates + left);
                if (!returnCells.Contains(node))
                {
                    returnCells.Add(node);
                }
            } //left
            if (rootNode.movementPointsLeft - HexGrid.instance.hexCells[(rootNode.coordinates + right).X, (rootNode.coordinates + right).Z].movementCost >= 0) 
            {
                MovementNode node = new MovementNode(rootNode, rootNode.movementPointsLeft, rootNode.coordinates + right);
                if (!returnCells.Contains(node))
                {
                    returnCells.Add(node);
                }
            } //right
        }
    }

    public class MovementNode
    {
        public HexCoordinates coordinates;
        public MovementNode prevNode;
        public int movementPointsLeft;

        public MovementNode(MovementNode prevNode, int movementPointsPreMove, HexCoordinates coordinates) 
        {
            this.prevNode = prevNode;
            this.movementPointsLeft = movementPointsPreMove - HexGrid.instance.hexCells[coordinates.X, coordinates.Z].movementCost;
            this.coordinates = coordinates;
        }

        public bool Equals(MovementNode other)
        {
            if (other == null) { return false; }

            if (this.coordinates == other.coordinates)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}