using System.Collections.Generic;
using System.IO;
using YamlDotNet.RepresentationModel;
using Unity.VisualScripting;
using System;

public class Map
{
    protected string fileLocation;

    public List<HexCellAbstract> allCells;

    int width = 100;
    int height = 100;

    HexCoordinates bottomLeftCoords;
    HexCoordinates bottomRightCoords;
    HexCoordinates topLeftCoords;
    HexCoordinates topRightCoords;

    public Map(string fileLocation) 
    {
        this.fileLocation = fileLocation;
        LoadMap();
    }

    public void LoadMap() 
    {
        allCells = new List<HexCellAbstract>();

        string mapText = System.IO.File.ReadAllText(fileLocation);

        var input = new StringReader(mapText);

        var yaml = new YamlStream();
        yaml.Load(input);

        var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;
        var items = (YamlSequenceNode)mapping.Children[new YamlScalarNode("HexCells")];
        foreach (YamlMappingNode item in items)
        {
            string terrain = item.Children[new YamlScalarNode("terrain")].ConvertTo<string>();
            string xString = item.Children[new YamlScalarNode("x")].ConvertTo<string>();
            string zString = item.Children[new YamlScalarNode("z")].ConvertTo<string>();
            string heightString = item.Children[new YamlScalarNode("height")].ConvertTo<string>();

            int x = Int16.Parse(xString);
            int z = Int16.Parse(zString);
            float height = float.Parse(heightString);

            allCells.Add(new HexCellAbstract(terrain, new HexCoordinates(x, z, height)));
        }

        int lowestX = allCells[0].coordinates.X;
        int highestX = allCells[0].coordinates.X;
        int lowestZ = allCells[0].coordinates.Z;
        int highestZ = allCells[0].coordinates.Z;
        foreach (HexCellAbstract cell in allCells) 
        {
            if (cell.coordinates.X < lowestX) { lowestX = cell.coordinates.X; }
            if (cell.coordinates.X > highestX) { highestX = cell.coordinates.X; }
            if (cell.coordinates.Z < lowestZ) { lowestZ = cell.coordinates.Z; }
            if (cell.coordinates.Z > highestZ) { highestZ = cell.coordinates.Z; }
        }
        width = (highestX - lowestX) + 1;
        height = (highestZ - lowestZ) + 1;

        int lowestXAtBottom = 100;
        foreach (HexCellAbstract cell in allCells)
        {
            if (cell.coordinates.Z == lowestZ)
            {
                if (cell.coordinates.X < lowestXAtBottom) 
                {
                    lowestXAtBottom = cell.coordinates.X;
                }
            }
        }
        int highestXAtTop = 0;
        foreach (HexCellAbstract cell in allCells)
        {
            if (cell.coordinates.Z == highestZ)
            {
                if (cell.coordinates.X > highestXAtTop)
                {
                    highestXAtTop = cell.coordinates.X;
                }
            }
        }

        bottomLeftCoords = new HexCoordinates(lowestXAtBottom, lowestZ);
        bottomRightCoords = new HexCoordinates(highestX, lowestZ);
        topLeftCoords = new HexCoordinates(lowestX, highestZ);
        topRightCoords = new HexCoordinates(highestXAtTop, highestZ);
    }

    public HexCellAbstract[] GetCells() { return allCells.ToArray(); }

    public HexCoordinates GetBottomLeftCoords() { return bottomLeftCoords; }
    public HexCoordinates GetBottomRightCoords() { return bottomRightCoords; }
    public HexCoordinates GetTopLeftCoords() { return topLeftCoords; }
    public HexCoordinates GetTopRightCoords() { return topRightCoords; }

    public int GetWidth() { return width; }
    public int GetHeight() { return height; }
}