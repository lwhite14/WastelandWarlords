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

    public bool canClick { get; set; } = true;
    public HexCell[,] hexCells;
    public LayerMask IgnoreMe;

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
            if (cell.type == "Water") { CreateCell(cell.coordinates, ResourceFactory.HexCellWater); }
            if (cell.type == "Plains") { CreateCell(cell.coordinates, ResourceFactory.HexCellPlains); }
            if (cell.type == "Forest") { CreateCell(cell.coordinates, ResourceFactory.HexCellForest); }
            if (cell.type == "ImpactSite") { CreateCell(cell.coordinates, ResourceFactory.HexCellImpactSite); }
        }

        GameState.Units.Add(Instantiate<Unit>(ResourceFactory.Unit));
        GameState.Units[0].SetCell(hexCells[23, 4]);
        GameState.Units[0].unitName = "Lucian";

        GameState.Enemies.Add(Instantiate<Enemy>(ResourceFactory.Enemy));
        GameState.Enemies[0].SetCell(hexCells[17, 12]);
        GameState.Enemies[0].enemyName = "Geeker";

        GameState.Enemies.Add(Instantiate<Enemy>(ResourceFactory.Enemy));
        GameState.Enemies[1].SetCell(hexCells[18, 23]);
        GameState.Enemies[1].enemyName = "Geeker";

        GameState.Enemies.Add(Instantiate<Enemy>(ResourceFactory.Enemy));
        GameState.Enemies[2].SetCell(hexCells[19, 22]);
        GameState.Enemies[2].enemyName = "Geeker";

        GameState.Settlements.Add(Instantiate<Settlement>(ResourceFactory.Settlement));
        GameState.Settlements[0].SetCell(hexCells[18, 4]);
        GameState.Settlements[0].settlementName = "Grapguard";

        GameState.Collectables.Add(Instantiate<Collectable>(ResourceFactory.Battery));
        GameState.Collectables[0].SetCell(hexCells[37, 12]);
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && canClick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(inputRay, out hit, 1000f, ~IgnoreMe))
                {
                    if (hit.transform.gameObject.GetComponentInParent<HexCell>() != null)
                    {
                        if (hit.transform.gameObject.GetComponentInParent<HexCell>() != GameState.CellSelected)
                        {
                            if (GameState.CellSelected != null) { GameState.CellSelected.Unselect(); }
                            DestroyMarkers();
                            GameState.CellSelected = hit.transform.gameObject.GetComponentInParent<HexCell>();
                            ResetMarkerCellLists();
                            GameState.CellSelected.Select();
                        }
                    }
                }
            }
            if (Input.GetMouseButtonDown(1) && canClick) 
            {
                Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(inputRay, out hit, 1000f, ~IgnoreMe))
                {
                    if (hit.transform.gameObject.GetComponentInParent<HexCell>() != null)
                    {
                        if (hit.transform.gameObject.GetComponentInParent<HexCell>() != GameState.CellSelected)
                        {
                            HexCell prevCellSelected = GameState.CellSelected;
                            bool isCellSelected = false;
                            GameState.CellSelected = hit.transform.gameObject.GetComponentInParent<HexCell>();
                            foreach (HexCell cell in GameState.CellsMovement)
                            {
                                if (cell.coordinates == GameState.CellSelected.coordinates)
                                {
                                    DestroyMarkers();
                                    GameState.CellSelected = hit.transform.gameObject.GetComponentInParent<HexCell>();
                                    GameState.UnitSelected.Move(GameState.CellSelected);
                                    ResetMarkerCellLists();
                                    GameState.CellSelected.Select();
                                    isCellSelected = true;
                                }
                            }
                            foreach (HexCell cell in GameState.CellsAttack)
                            {
                                if (cell.coordinates == GameState.CellSelected.coordinates)
                                {
                                    DestroyMarkers();
                                    GameState.CellSelected = hit.transform.gameObject.GetComponentInParent<HexCell>();
                                    GameState.UnitSelected.Attack(GameState.CellSelected);
                                    ResetMarkerCellLists();
                                    GameState.CellSelected.Select();
                                    isCellSelected = true;
                                }
                            }
                            if (!isCellSelected)
                            {
                                GameState.CellSelected = prevCellSelected;
                            }
                        }
                    }
                }
            }
        }
    }

    void CreateCell(HexCoordinates coords, HexCell celltype)
    {
        Vector3 position = coords.ToWorldSpace();

        HexCell cell = hexCells[coords.X, coords.Z] = Instantiate<HexCell>(celltype);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = coords;
    }

    public static void ResetMarkerCellLists() 
    {
        GameState.UnitSelected = null;
        GameState.CellsMovement = new List<HexCell>();
        GameState.CellsAttack = new List<HexCell>();
    }

    public static void DestroyMarkers() 
    {
        foreach (GameObject marker in GameObject.FindGameObjectsWithTag("MovementMarker")) { Destroy(marker); }
        foreach (GameObject marker in GameObject.FindGameObjectsWithTag("SelectionMarker")) { Destroy(marker); }
        foreach (GameObject marker in GameObject.FindGameObjectsWithTag("AttackMarker")) { Destroy(marker); }
    }
}
