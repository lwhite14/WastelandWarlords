using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraControls : MonoBehaviour
{
    public static CameraControls instance = null;

    public float moveSpeed = 10.0f;
    public float scrollSpeed = 10.0f;

    float minCameraY = 30.0f;
    float maxCameraY = 200.0f;
    float yConstant = 100.0f;

    public float minCameraX { private get; set; }
    public float maxCameraX { private get; set; }
    public float minCameraZ { private get; set; }
    public float maxCameraZ { private get; set; }

    void Awake()
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
        minCameraX = CurrentMap.instance.currentMap.GetBottomLeftCoords().X * (HexMetrics.innerRadius * 2f) + (HexMetrics.innerRadius * CurrentMap.instance.currentMap.GetBottomLeftCoords().Z); 
        maxCameraX = CurrentMap.instance.currentMap.GetBottomRightCoords().X * (HexMetrics.innerRadius * 2f) + (HexMetrics.innerRadius * CurrentMap.instance.currentMap.GetBottomRightCoords().Z);
        minCameraZ = CurrentMap.instance.currentMap.GetBottomLeftCoords().Z * (HexMetrics.outerRadius * 1.5f);
        maxCameraZ = CurrentMap.instance.currentMap.GetTopLeftCoords().Z * (HexMetrics.outerRadius * 1.5f);
        transform.position = new Vector3(minCameraX + ((maxCameraX - minCameraX) / 2), transform.position.y, minCameraZ + ((maxCameraZ - minCameraZ) / 2));
    }

    void Update()
    {
        float yPos = transform.position.y;
        yPos -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * scrollSpeed * yConstant;
        yPos = Mathf.Clamp(yPos, minCameraY, maxCameraY);

        float xPos = transform.position.x;
        float zPos = transform.position.z;
        if (Input.GetKey(KeyCode.W))
        {
            zPos += moveSpeed * Time.deltaTime * (yPos / yConstant);
        }
        if (Input.GetKey(KeyCode.S))
        {
            zPos -= moveSpeed * Time.deltaTime * (yPos / yConstant);
        }
        if (Input.GetKey(KeyCode.A))
        {
            xPos -= moveSpeed * Time.deltaTime * (yPos / yConstant);
        }
        if (Input.GetKey(KeyCode.D))
        {
            xPos += moveSpeed * Time.deltaTime * (yPos / yConstant);
        }
        xPos = Mathf.Clamp(xPos, minCameraX, maxCameraX);
        zPos = Mathf.Clamp(zPos, minCameraZ, maxCameraZ);

        transform.position = new Vector3(xPos, yPos, zPos);
    }
}
