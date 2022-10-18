using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    public static FogOfWar Instance = null;

    public LayerMask fogLayer;

    Mesh mesh;
    Vector3[] vertices;
    Color[] colours;

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
        Initialize();
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
                    float alpha = 0;
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
            colours[i] = Color.white;
        }
        UpdateColour();
    }

    void UpdateColour() 
    {
        mesh.colors = colours;
    }

}
