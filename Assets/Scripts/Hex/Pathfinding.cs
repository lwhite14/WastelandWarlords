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

            MovementNode initialNode = new MovementNode(null, unusedMovementPoints, startingCell.coordinates, true);
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

        public MovementNode(MovementNode prevNode, int movementPointsPreMove, HexCoordinates coordinates, bool rootNode = false) 
        {
            this.prevNode = prevNode;
            if (rootNode)   { this.movementPointsLeft = movementPointsPreMove; }
            else            { this.movementPointsLeft = movementPointsPreMove - HexGrid.instance.hexCells[coordinates.X, coordinates.Z].movementCost; }
            this.coordinates = coordinates;
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null) { return false; }

            MovementNode movementNodeObj = obj as MovementNode;
            if (this.coordinates == movementNodeObj.coordinates)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator ==(MovementNode node1, MovementNode node2)
        {
            if (ReferenceEquals(node1, node2)) return true;

            if (ReferenceEquals(node1, null) || ReferenceEquals(node2, null)) return false;

            if ((node1.coordinates.X == node2.coordinates.X) && (node1.coordinates.Z == node2.coordinates.Z))
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(MovementNode node1, MovementNode node2)
        {
            if (ReferenceEquals(node1, node2)) { return false; }

            if (ReferenceEquals(node1, null) || ReferenceEquals(node2, null)) { return true; }

            if ((node1.coordinates.X == node2.coordinates.X) && (node1.coordinates.Z == node2.coordinates.Z))
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return this.coordinates.X.GetHashCode() + this.coordinates.Z.GetHashCode();
        }
    }
}