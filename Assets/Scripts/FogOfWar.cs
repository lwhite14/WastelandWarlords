using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    public static FogOfWar instance = null;

    public Color fogColour;
    public LayerMask fogLayer;

    Mesh mesh;
    Vector3[] vertices;
    Color[] colours;

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
        Initialize();
    }

    void Update()
    {
        foreach (Unit unit in GameState.Units) 
        {
            CalculateVertexAlphas(unit.transform.position, new Vector3(unit.transform.position.x, unit.transform.position.y + 200.0f, unit.transform.position.z), unit.sightRange);
        }
        foreach (Settlement settlement in GameState.Settlements)
        {
            CalculateVertexAlphas(settlement.transform.position, new Vector3(settlement.transform.position.x, settlement.transform.position.y + 200.0f, settlement.transform.position.z), settlement.sightRange);
        }
    }

    public void CalculateVertexAlphas(Vector3 point, Vector3 raySpawn, float sightRange) 
    {
        Ray r = new Ray(raySpawn, point - raySpawn);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, 1000, fogLayer, QueryTriggerInteraction.Collide))
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 v = transform.TransformPoint(vertices[i]);
                float dist = Vector3.SqrMagnitude(v - hit.point);
                if (dist < (sightRange * sightRange))
                {
                    float alpha = Mathf.Min(colours[i].a, dist / (sightRange * sightRange));
                    colours[i].a = alpha;
                }
            }
            UpdateColour();
        }
    }

    void Initialize() 
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        colours = new Color[vertices.Length];

        for (int i = 0; i < colours.Length; i++)
        {
            colours[i] = fogColour;
        }
        UpdateColour();
    }

    void UpdateColour() 
    {
        mesh.colors = colours;
    }

}
