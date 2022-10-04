using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class HexGrid : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    HexCell[] hexCells;

    void Start()
    {
        hexCells = new HexCell[width * height];
        int i = 0;
        foreach (HexCoordinates coords in EnglishChannelMap.waterCoords) 
        {
            CreateCell(coords, i++, HexTypes.GetWater());
        }
        foreach (HexCoordinates coords in EnglishChannelMap.plainsCoords)
        {
            CreateCell(coords, i++, HexTypes.GetPlains());
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
                    Debug.Log(hit.transform.gameObject.GetComponentInParent<HexCell>().coordinates);
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
