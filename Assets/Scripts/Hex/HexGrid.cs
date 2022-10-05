using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class HexGrid : MonoBehaviour
{
    public static HexGrid instance = null;

    public int width = 10;
    public int height = 10;

    HexCell[] hexCells;
    HexCell selectedCell = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        hexCells = new HexCell[width * height];
        int i = 0;
        foreach (HexCoordinates coords in CurrentMap.instance.currentMap.GetWaterCoords())
        {
            CreateCell(coords, i++, HexTypes.GetWater());
        }
        foreach (HexCoordinates coords in CurrentMap.instance.currentMap.GetPlainsCoords())
        {
            CreateCell(coords, i++, HexTypes.GetPlains());
        }
        foreach (HexCoordinates coords in CurrentMap.instance.currentMap.GetForestCoords())
        {
            CreateCell(coords, i++, HexTypes.GetForest());
        }
        foreach (HexCoordinates coords in CurrentMap.instance.currentMap.GetInpactSiteCoords())
        {
            CreateCell(coords, i++, HexTypes.GetImpactSite());
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(inputRay, out hit))
            {
                if (hit.transform.gameObject.GetComponentInParent<HexCell>() != null)
                {
                    if (hit.transform.gameObject.GetComponentInParent<HexCell>() != selectedCell) 
                    {
                        if (selectedCell != null)
                        {
                            selectedCell.Unselect();
                        }
                        selectedCell = hit.transform.gameObject.GetComponentInParent<HexCell>();
                        selectedCell.Select();
                    }
                }
            }
        }
    }

    void CreateCell(HexCoordinates coords, int i, HexCell celltype)
    {
        Vector3 position;
        position.x = coords.X * (HexMetrics.innerRadius * 2f) + (HexMetrics.innerRadius * coords.Z);
        position.y = 0f;
        position.z = coords.Z * (HexMetrics.outerRadius * 1.5f);

        HexCell cell = hexCells[i] = Instantiate<HexCell>(celltype);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = coords;
    }
}
