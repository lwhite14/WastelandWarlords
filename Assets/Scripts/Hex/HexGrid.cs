using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class HexGrid : MonoBehaviour
{
    public static HexGrid Instance = null;

    public bool canClick { get; set; } = true;
    public HexCell[,] hexCells;
    public LayerMask IgnoreMe;

    [Header("Unit Data")]
    public List<HexCoordinates> unitCoords;
    public List<string> unitNames;

    [Header("Enemy Data")]
    public List<HexCoordinates> enemyCoords;
    public List<string> enemyNames;

    [Header("Settlement Data")]
    public List<HexCoordinates> settlementCoords;
    public List<string> settlementNames;

    [Header("Collectable Data")]
    public List<HexCoordinates> collectableCoords;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        hexCells = new HexCell[CurrentMap.Instance.currentMap.GetWidth(), CurrentMap.Instance.currentMap.GetHeight()];
        foreach (HexCellAbstract cell in CurrentMap.Instance.currentMap.GetCells())
        {
            if (cell.type == "Water") { CreateCell(cell.coordinates, ResourceFactory.HexCellWater); }
            if (cell.type == "Plains") { CreateCell(cell.coordinates, ResourceFactory.HexCellPlains); }
            if (cell.type == "Forest") { CreateCell(cell.coordinates, ResourceFactory.HexCellForest); }
            if (cell.type == "ImpactSite") { CreateCell(cell.coordinates, ResourceFactory.HexCellImpactSite); }
        }

        for (int i = 0; i < unitCoords.Count; i++) 
        {
            GameState.Units.Add(Instantiate<Unit>(ResourceFactory.Unit));
            GameState.Units[i].SetCell(hexCells[unitCoords[i].X, unitCoords[i].Z]);
            GameState.Units[i].unitName = unitNames[i];
        }
        for (int i = 0; i < enemyCoords.Count; i++)
        {
            GameState.Enemies.Add(Instantiate<Enemy>(ResourceFactory.Enemy));
            GameState.Enemies[i].SetCell(hexCells[enemyCoords[i].X, enemyCoords[i].Z]);
            GameState.Enemies[i].enemyName = enemyNames[i];
        }
        for (int i = 0; i < settlementCoords.Count; i++)
        {
            GameState.Settlements.Add(Instantiate<Settlement>(ResourceFactory.Settlement));
            GameState.Settlements[i].SetCell(hexCells[settlementCoords[i].X, settlementCoords[i].Z]);
            GameState.Settlements[i].settlementName = settlementNames[i];
        }
        for (int i = 0; i < collectableCoords.Count; i++)
        {
            GameState.Collectables.Add(Instantiate<Collectable>(ResourceFactory.Battery));
            GameState.Collectables[i].SetCell(hexCells[collectableCoords[i].X, collectableCoords[i].Z]);
        }
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
