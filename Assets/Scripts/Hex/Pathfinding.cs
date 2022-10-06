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
            if (((rootNode.coordinates + topRight).X >= 0) && ((rootNode.coordinates + topRight).X <= CurrentMap.instance.currentMap.GetWidth() - 1))
            {
                if (((rootNode.coordinates + topRight).Z >= 0) && ((rootNode.coordinates + topRight).Z <= CurrentMap.instance.currentMap.GetHeight() - 1))
                {
                    if (HexGrid.instance.hexCells[(rootNode.coordinates + topRight).X, (rootNode.coordinates + topRight).Z] != null)
                    {
                        if (rootNode.movementPointsLeft - HexGrid.instance.hexCells[(rootNode.coordinates + topRight).X, (rootNode.coordinates + topRight).Z].movementCost >= 0)
                        {
                            MovementNode node = new MovementNode(rootNode, rootNode.movementPointsLeft, rootNode.coordinates + topRight);
                            if (!returnCells.Contains(node))
                            {
                                returnCells.Add(node);
                            }
                        } //top right
                    }
                }
            }

            if (((rootNode.coordinates + topLeft).X >= 0) && ((rootNode.coordinates + topLeft).X <= CurrentMap.instance.currentMap.GetWidth() - 1))
            {
                if (((rootNode.coordinates + topLeft).Z >= 0) && ((rootNode.coordinates + topLeft).Z <= CurrentMap.instance.currentMap.GetHeight() - 1))
                {
                    if (HexGrid.instance.hexCells[(rootNode.coordinates + topLeft).X, (rootNode.coordinates + topLeft).Z] != null)
                    {
                        if (rootNode.movementPointsLeft - HexGrid.instance.hexCells[(rootNode.coordinates + topLeft).X, (rootNode.coordinates + topLeft).Z].movementCost >= 0)
                        {
                            MovementNode node = new MovementNode(rootNode, rootNode.movementPointsLeft, rootNode.coordinates + topLeft);
                            if (!returnCells.Contains(node))
                            {
                                returnCells.Add(node);
                            }
                        } //top left
                    }
                }
            }

            if (((rootNode.coordinates + bottomRight).X >= 0) && ((rootNode.coordinates + bottomRight).X <= CurrentMap.instance.currentMap.GetWidth() - 1))
            {
                if (((rootNode.coordinates + bottomRight).Z >= 0) && ((rootNode.coordinates + bottomRight).Z <= CurrentMap.instance.currentMap.GetHeight() - 1))
                {
                    if (HexGrid.instance.hexCells[(rootNode.coordinates + bottomRight).X, (rootNode.coordinates + bottomRight).Z] != null)
                    {
                        if (rootNode.movementPointsLeft - HexGrid.instance.hexCells[(rootNode.coordinates + bottomRight).X, (rootNode.coordinates + bottomRight).Z].movementCost >= 0)
                        {
                            MovementNode node = new MovementNode(rootNode, rootNode.movementPointsLeft, rootNode.coordinates + bottomRight);
                            if (!returnCells.Contains(node))
                            {
                                returnCells.Add(node);
                            }
                        } //bottom right
                    }
                }
            }

            if (((rootNode.coordinates + bottomLeft).X >= 0) && ((rootNode.coordinates + bottomLeft).X <= CurrentMap.instance.currentMap.GetWidth() - 1))
            {
                if (((rootNode.coordinates + bottomLeft).Z >= 0) && ((rootNode.coordinates + bottomLeft).Z <= CurrentMap.instance.currentMap.GetHeight() - 1))
                {
                    if (HexGrid.instance.hexCells[(rootNode.coordinates + bottomLeft).X, (rootNode.coordinates + bottomLeft).Z] != null)
                    {
                        if (rootNode.movementPointsLeft - HexGrid.instance.hexCells[(rootNode.coordinates + bottomLeft).X, (rootNode.coordinates + bottomLeft).Z].movementCost >= 0)
                        {
                            MovementNode node = new MovementNode(rootNode, rootNode.movementPointsLeft, rootNode.coordinates + bottomLeft);
                            if (!returnCells.Contains(node))
                            {
                                returnCells.Add(node);
                            }
                        } //bottom left
                    }
                }
            }

            if (((rootNode.coordinates + left).X >= 0) && ((rootNode.coordinates + left).X <= CurrentMap.instance.currentMap.GetWidth() - 1))
            {
                if (((rootNode.coordinates + left).Z >= 0) && ((rootNode.coordinates + left).Z <= CurrentMap.instance.currentMap.GetHeight() - 1))
                {
                    if (HexGrid.instance.hexCells[(rootNode.coordinates + left).X, (rootNode.coordinates + left).Z] != null)
                    {
                        if (rootNode.movementPointsLeft - HexGrid.instance.hexCells[(rootNode.coordinates + left).X, (rootNode.coordinates + left).Z].movementCost >= 0)
                        {
                            MovementNode node = new MovementNode(rootNode, rootNode.movementPointsLeft, rootNode.coordinates + left);
                            if (!returnCells.Contains(node))
                            {
                                returnCells.Add(node);
                            }
                        } //left
                    }
                }
            }

            if (((rootNode.coordinates + right).X >= 0) && ((rootNode.coordinates + right).X <= CurrentMap.instance.currentMap.GetWidth() - 1))
            {
                if (((rootNode.coordinates + right).Z >= 0) && ((rootNode.coordinates + right).Z <= CurrentMap.instance.currentMap.GetHeight() - 1))
                {
                    if (HexGrid.instance.hexCells[(rootNode.coordinates + right).X, (rootNode.coordinates + right).Z] != null)
                    {
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
            }
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