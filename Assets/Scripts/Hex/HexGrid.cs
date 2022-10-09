using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using System.IO;
using Unity.VisualScripting;

public class HexGrid : MonoBehaviour
{
    public static HexGrid instance = null;

    public HexCell[,] hexCells;

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
        hexCells = new HexCell[CurrentMap.instance.currentMap.GetWidth(), CurrentMap.instance.currentMap.GetHeight()];
        foreach (HexCellAbstract cell in CurrentMap.instance.currentMap.GetCells())
        {
            if (cell.type == "Water") { CreateCell(cell.coordinates, ResourceFactory.HexCellWater, cell.height); }
            if (cell.type == "Plains") { CreateCell(cell.coordinates, ResourceFactory.HexCellPlains, cell.height); }
            if (cell.type == "Forest") { CreateCell(cell.coordinates, ResourceFactory.HexCellForest, cell.height); }
            if (cell.type == "ImpactSite") { CreateCell(cell.coordinates, ResourceFactory.HexCellImpactSite, cell.height); }
        }

        GameState.Units.Add(Instantiate<Unit>(ResourceFactory.Unit));
        GameState.Units[0].SetCell(hexCells[17, 13]);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(inputRay, out hit))
            {
                if (hit.transform.gameObject.GetComponentInParent<HexCell>() != null)
                {
                    if (hit.transform.gameObject.GetComponentInParent<HexCell>() != GameState.CellSelected) 
                    {
                        if (GameState.CellSelected != null)
                        {
                            GameState.CellSelected.Unselect();
                            foreach (GameObject marker in GameObject.FindGameObjectsWithTag("MovementMarker")) 
                            {
                                Destroy(marker);
                            }
                        }
                        GameState.CellSelected = hit.transform.gameObject.GetComponentInParent<HexCell>();
                        foreach (HexCell cell in GameState.CellsMovement) 
                        {
                            if (cell.coordinates == GameState.CellSelected.coordinates) 
                            {
                                GameState.UnitSelected.Move(GameState.CellSelected);
                            }
                        }
                        GameState.UnitSelected = null;
                        GameState.CellsMovement = new List<HexCell>();
                        GameState.CellSelected.Select();
                    }
                }
            }
        }
    }

    void CreateCell(HexCoordinates coords, HexCell celltype, float height)
    {
        Vector3 position;
        position.x = coords.X * (HexMetrics.innerRadius * 2f) + (HexMetrics.innerRadius * coords.Z);
        position.y = height * 8;
        position.z = coords.Z * (HexMetrics.outerRadius * 1.5f);

        HexCell cell = hexCells[coords.X, coords.Z] = Instantiate<HexCell>(celltype);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = coords;
    }
}
