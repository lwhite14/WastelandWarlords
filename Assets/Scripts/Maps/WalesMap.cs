using UnityEngine;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using System.IO;
using YamlDotNet.RepresentationModel;
using static UnityEditor.Rendering.CameraUI;
using YamlDotNet.Serialization.ValueDeserializers;
using Unity.VisualScripting;
using System;

public class WalesMap : Map
{
    public List<HexCellAbstract> allCells;

    int width = 100;
    int height = 100;

    public WalesMap() 
    {
        allCells = new List<HexCellAbstract>();

        string mapText = System.IO.File.ReadAllText(@"Assets\Maps\WalesMap.yaml");

        // Setup the input
        var input = new StringReader(mapText);

        // Load the stream
        var yaml = new YamlStream();
        yaml.Load(input);

        // Examine the stream
        var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;
        var items = (YamlSequenceNode)mapping.Children[new YamlScalarNode("HexCells")];
        foreach (YamlMappingNode item in items)
        {
            string terrain = item.Children[new YamlScalarNode("terrain")].ConvertTo<string>();
            string xString = item.Children[new YamlScalarNode("x")].ConvertTo<string>();
            string zString = item.Children[new YamlScalarNode("z")].ConvertTo<string>();

            int x = Int16.Parse(xString);
            int z = Int16.Parse(zString);

            allCells.Add(new HexCellAbstract(terrain, new HexCoordinates(x, z)));

        }
    }

    public HexCellAbstract[] GetCells() { return allCells.ToArray(); }

    public HexCoordinates GetBottomLeftCoords() { return new HexCoordinates(13, 0); }
    public HexCoordinates GetBottomRightCoords() { return new HexCoordinates(32, 0); }
    public HexCoordinates GetTopLeftCoords() { return new HexCoordinates(0, 27); }
    public HexCoordinates GetTopRightCoords() { return new HexCoordinates(18, 27); }

    public int GetWidth() { return width; }
    public int GetHeight() { return height; }
}