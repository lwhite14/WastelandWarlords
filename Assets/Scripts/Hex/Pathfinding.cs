using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace HexPathfinding
{
    public static class MovementFinder
    {
        public static HexCoordinates TopRight { get; }      = new HexCoordinates(0, 1);
        public static HexCoordinates TopLeft { get; }       = new HexCoordinates(-1, 1);
        public static HexCoordinates BottomRight { get; }   = new HexCoordinates(1, -1);
        public static HexCoordinates BottomLeft { get; }    = new HexCoordinates(0, -1);
        public static HexCoordinates Left { get; }          = new HexCoordinates(-1, 0);
        public static HexCoordinates Right { get; }         = new HexCoordinates(1, 0);


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
            if (((rootNode.coordinates + TopRight).X >= 0) && ((rootNode.coordinates + TopRight).X <= CurrentMap.instance.currentMap.GetWidth() - 1))
            {
                if (((rootNode.coordinates + TopRight).Z >= 0) && ((rootNode.coordinates + TopRight).Z <= CurrentMap.instance.currentMap.GetHeight() - 1))
                {
                    if (HexGrid.instance.hexCells[(rootNode.coordinates + TopRight).X, (rootNode.coordinates + TopRight).Z] != null)
                    {
                        if (rootNode.movementPointsLeft - HexGrid.instance.hexCells[(rootNode.coordinates + TopRight).X, (rootNode.coordinates + TopRight).Z].movementCost >= 0)
                        {
                            MovementNode node = new MovementNode(rootNode, rootNode.movementPointsLeft, rootNode.coordinates + TopRight);
                            if (!returnCells.Contains(node))
                            {
                                returnCells.Add(node);
                            }
                        } //top right
                    }
                }
            }

            if (((rootNode.coordinates + TopLeft).X >= 0) && ((rootNode.coordinates + TopLeft).X <= CurrentMap.instance.currentMap.GetWidth() - 1))
            {
                if (((rootNode.coordinates + TopLeft).Z >= 0) && ((rootNode.coordinates + TopLeft).Z <= CurrentMap.instance.currentMap.GetHeight() - 1))
                {
                    if (HexGrid.instance.hexCells[(rootNode.coordinates + TopLeft).X, (rootNode.coordinates + TopLeft).Z] != null)
                    {
                        if (rootNode.movementPointsLeft - HexGrid.instance.hexCells[(rootNode.coordinates + TopLeft).X, (rootNode.coordinates + TopLeft).Z].movementCost >= 0)
                        {
                            MovementNode node = new MovementNode(rootNode, rootNode.movementPointsLeft, rootNode.coordinates + TopLeft);
                            if (!returnCells.Contains(node))
                            {
                                returnCells.Add(node);
                            }
                        } //top left
                    }
                }
            }

            if (((rootNode.coordinates + BottomRight).X >= 0) && ((rootNode.coordinates + BottomRight).X <= CurrentMap.instance.currentMap.GetWidth() - 1))
            {
                if (((rootNode.coordinates + BottomRight).Z >= 0) && ((rootNode.coordinates + BottomRight).Z <= CurrentMap.instance.currentMap.GetHeight() - 1))
                {
                    if (HexGrid.instance.hexCells[(rootNode.coordinates + BottomRight).X, (rootNode.coordinates + BottomRight).Z] != null)
                    {
                        if (rootNode.movementPointsLeft - HexGrid.instance.hexCells[(rootNode.coordinates + BottomRight).X, (rootNode.coordinates + BottomRight).Z].movementCost >= 0)
                        {
                            MovementNode node = new MovementNode(rootNode, rootNode.movementPointsLeft, rootNode.coordinates + BottomRight);
                            if (!returnCells.Contains(node))
                            {
                                returnCells.Add(node);
                            }
                        } //bottom right
                    }
                }
            }

            if (((rootNode.coordinates + BottomLeft).X >= 0) && ((rootNode.coordinates + BottomLeft).X <= CurrentMap.instance.currentMap.GetWidth() - 1))
            {
                if (((rootNode.coordinates + BottomLeft).Z >= 0) && ((rootNode.coordinates + BottomLeft).Z <= CurrentMap.instance.currentMap.GetHeight() - 1))
                {
                    if (HexGrid.instance.hexCells[(rootNode.coordinates + BottomLeft).X, (rootNode.coordinates + BottomLeft).Z] != null)
                    {
                        if (rootNode.movementPointsLeft - HexGrid.instance.hexCells[(rootNode.coordinates + BottomLeft).X, (rootNode.coordinates + BottomLeft).Z].movementCost >= 0)
                        {
                            MovementNode node = new MovementNode(rootNode, rootNode.movementPointsLeft, rootNode.coordinates + BottomLeft);
                            if (!returnCells.Contains(node))
                            {
                                returnCells.Add(node);
                            }
                        } //bottom left
                    }
                }
            }

            if (((rootNode.coordinates + Left).X >= 0) && ((rootNode.coordinates + Left).X <= CurrentMap.instance.currentMap.GetWidth() - 1))
            {
                if (((rootNode.coordinates + Left).Z >= 0) && ((rootNode.coordinates + Left).Z <= CurrentMap.instance.currentMap.GetHeight() - 1))
                {
                    if (HexGrid.instance.hexCells[(rootNode.coordinates + Left).X, (rootNode.coordinates + Left).Z] != null)
                    {
                        if (rootNode.movementPointsLeft - HexGrid.instance.hexCells[(rootNode.coordinates + Left).X, (rootNode.coordinates + Left).Z].movementCost >= 0)
                        {
                            MovementNode node = new MovementNode(rootNode, rootNode.movementPointsLeft, rootNode.coordinates + Left);
                            if (!returnCells.Contains(node))
                            {
                                returnCells.Add(node);
                            }
                        } //left
                    }
                }
            }

            if (((rootNode.coordinates + Right).X >= 0) && ((rootNode.coordinates + Right).X <= CurrentMap.instance.currentMap.GetWidth() - 1))
            {
                if (((rootNode.coordinates + Right).Z >= 0) && ((rootNode.coordinates + Right).Z <= CurrentMap.instance.currentMap.GetHeight() - 1))
                {
                    if (HexGrid.instance.hexCells[(rootNode.coordinates + Right).X, (rootNode.coordinates + Right).Z] != null)
                    {
                        if (rootNode.movementPointsLeft - HexGrid.instance.hexCells[(rootNode.coordinates + Right).X, (rootNode.coordinates + Right).Z].movementCost >= 0)
                        {
                            MovementNode node = new MovementNode(rootNode, rootNode.movementPointsLeft, rootNode.coordinates + Right);
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