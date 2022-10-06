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
        hexCells = new HexCell[CurrentMap.instance.currentMap.GetWidth(), CurrentMap.instance.currentMap.GetHeight()];
        foreach (HexCellAbstract cell in CurrentMap.instance.currentMap.GetCells())
        {
            if (cell.type == "Water") { CreateCell(cell.coordinates, HexTypes.GetWater()); }
            if (cell.type == "Plains") { CreateCell(cell.coordinates, HexTypes.GetPlains()); }
            if (cell.type == "Forest") { CreateCell(cell.coordinates, HexTypes.GetForest()); }
            if (cell.type == "ImpactSite") { CreateCell(cell.coordinates, HexTypes.GetImpactSite()); }
        }

        Unit unit1 = Instantiate<Unit>(Resources.Load<Unit>("GameObjects/Unit"));
        unit1.SetCell(hexCells[21, 6]);

        Unit unit2 = Instantiate<Unit>(Resources.Load<Unit>("GameObjects/Unit"));
        unit2.SetCell(hexCells[17, 12]);

        Unit unit3 = Instantiate<Unit>(Resources.Load<Unit>("GameObjects/Unit"));
        unit3.SetCell(hexCells[13, 12]);        
        
        Unit unit4 = Instantiate<Unit>(Resources.Load<Unit>("GameObjects/Unit"));
        unit4.SetCell(hexCells[1, 27]);   
        
        Unit unit5 = Instantiate<Unit>(Resources.Load<Unit>("GameObjects/Unit"));
        unit5.SetCell(hexCells[19, 26]);  
        
        Unit unit6 = Instantiate<Unit>(Resources.Load<Unit>("GameObjects/Unit"));
        unit6.SetCell(hexCells[32, 0]); 
        
        Unit unit7 = Instantiate<Unit>(Resources.Load<Unit>("GameObjects/Unit"));
        unit7.SetCell(hexCells[13, 0]);
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
                    if (hit.transform.gameObject.GetComponentInParent<HexCell>() != selectedCell) 
                    {
                        if (selectedCell != null)
                        {
                            selectedCell.Unselect();
                            foreach (GameObject marker in GameObject.FindGameObjectsWithTag("MovementMarker")) 
                            {
                                Destroy(marker);
                            }
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
