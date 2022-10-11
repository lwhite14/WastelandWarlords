using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerrainPanel : MonoBehaviour
{
    public Text text;
    public Image image;

    public void UpdateTerrainPanel(HexCell selectedCell)
    {
        text.text = selectedCell.type + ": " + selectedCell.coordinates.ToString();
        if (selectedCell.type == "Forest") { image.sprite = ResourceFactory.ForestSprite; }
        if (selectedCell.type == "Plains") { image.sprite = ResourceFactory.PlainsSprite; }
        if (selectedCell.type == "Impact Site") { image.sprite = ResourceFactory.ImpactSiteSprite; }
        if (selectedCell.type == "Water") { image.sprite = ResourceFactory.WaterSprite; }
        if (selectedCell.type == "Shallow Water") { image.sprite = ResourceFactory.WaterShallowSprite; }
    }
}
