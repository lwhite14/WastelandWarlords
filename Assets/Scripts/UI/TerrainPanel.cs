using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerrainPanel : MonoBehaviour
{
    public Text text;
    public Image image;

    Sprite forestSprite;
    Sprite plainsSprite;
    Sprite impactSiteSprite;
    Sprite waterSprite;
    Sprite waterShallowSprite;

    void Awake()
    {
        forestSprite = Resources.Load<Sprite>("Textures/Forest");
        plainsSprite = Resources.Load<Sprite>("Textures/Plains");
        impactSiteSprite = Resources.Load<Sprite>("Textures/ImpactSite");
        waterSprite = Resources.Load<Sprite>("Textures/Water");
        waterShallowSprite = Resources.Load<Sprite>("Textures/WaterShallow");
    }

    public void UpdateTerrainPanel(HexCell selectedCell)
    {
        text.text = selectedCell.type + ": " + selectedCell.coordinates.ToString();
        if (selectedCell.type == "Forest") { image.sprite = forestSprite; }
        if (selectedCell.type == "Plains") { image.sprite = plainsSprite; }
        if (selectedCell.type == "Impact Site") { image.sprite = impactSiteSprite; }
        if (selectedCell.type == "Water") { image.sprite = waterSprite; }
        if (selectedCell.type == "Shallow Water") { image.sprite = waterShallowSprite; }
    }
}
