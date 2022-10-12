using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    public static FogOfWar instance = null;

    public GameObject fogOfWarPlane;
    public LayerMask fogLayer;
    public float radius = 5.0f;
    float radiusSqr { get { return radius * radius; } }

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
            CalculateVertexAlphas(unit.transform);
        }
        foreach (Settlement settlement in GameState.Settlements)
        {
            CalculateVertexAlphas(settlement.transform);
        }
    }

    void CalculateVertexAlphas(Transform point) 
    {
        Ray r = new Ray(transform.position, point.transform.position - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, 1000, fogLayer, QueryTriggerInteraction.Collide))
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 v = fogOfWarPlane.transform.TransformPoint(vertices[i]);
                float dist = Vector3.SqrMagnitude(v - hit.point);
                if (dist < radiusSqr)
                {
                    float alpha = Mathf.Min(colours[i].a, dist / radiusSqr);
                    colours[i].a = alpha;
                }
            }
            UpdateColour();
        }
    }

    void Initialize() 
    {
        mesh = fogOfWarPlane.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        colours = new Color[vertices.Length];

        for (int i = 0; i < colours.Length; i++) 
        {
            colours[i] = Color.grey;
        }
        UpdateColour();
    }

    void UpdateColour() 
    {
        mesh.colors = colours;
    }

    public void EndTurn() 
    {
        Initialize();
    }

}
