using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using System.IO;

public class HexGrid : MonoBehaviour
{
    public static HexGrid instance = null;

    public int width;
    public int height;

    HexCell[,] hexCells;
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
        hexCells = new HexCell[width, height];
        foreach (HexCellAbstract cell in CurrentMap.instance.currentMap.GetCells())
        {
            if (cell.type == "Water") { CreateCell(cell.coordinates, HexTypes.GetWater()); }
            if (cell.type == "Plains") { CreateCell(cell.coordinates, HexTypes.GetPlains()); }
            if (cell.type == "Forest") { CreateCell(cell.coordinates, HexTypes.GetForest()); }
            if (cell.type == "ImpactSite") { CreateCell(cell.coordinates, HexTypes.GetImpactSite()); }
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

    void CreateCell(HexCoordinates coords, HexCell celltype)
    {
        Vector3 position;
        //position.x = coords.X * (HexMetrics.innerRadius * 2f);
        position.x = coords.X * (HexMetrics.innerRadius * 2f) + (HexMetrics.innerRadius * coords.Z);
        position.y = 0f;
        position.z = coords.Z * (HexMetrics.outerRadius * 1.5f);

        HexCell cell = hexCells[coords.X, coords.Z] = Instantiate<HexCell>(celltype);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = coords;
    }
}
