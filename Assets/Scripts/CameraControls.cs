using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraControls : MonoBehaviour
{
    public static CameraControls Instance = null;

    public float moveSpeed = 10.0f;
    public float scrollSpeed = 10.0f;

    float minCameraY = 50.0f;
    float maxCameraY = 200.0f;
    float yConstant = 100.0f;

    public float minCameraX { private get; set; }
    public float maxCameraX { private get; set; }
    public float minCameraZ { private get; set; }
    public float maxCameraZ { private get; set; }

    public bool control { set; get; } = true;

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
        minCameraX = CurrentMap.Instance.currentMap.GetBottomLeftCoords().X * (HexMetrics.innerRadius * 2f) + (HexMetrics.innerRadius * CurrentMap.Instance.currentMap.GetBottomLeftCoords().Z); 
        maxCameraX = CurrentMap.Instance.currentMap.GetBottomRightCoords().X * (HexMetrics.innerRadius * 2f) + (HexMetrics.innerRadius * CurrentMap.Instance.currentMap.GetBottomRightCoords().Z);
        minCameraZ = CurrentMap.Instance.currentMap.GetBottomLeftCoords().Z * (HexMetrics.outerRadius * 1.5f);
        maxCameraZ = CurrentMap.Instance.currentMap.GetTopLeftCoords().Z * (HexMetrics.outerRadius * 1.5f);
        //transform.position = new Vector3(minCameraX + ((maxCameraX - minCameraX) / 2), transform.position.y, minCameraZ + ((maxCameraZ - minCameraZ) / 2));
        transform.position = new Vector3(GameState.Units[0].transform.position.x, transform.position.y, GameState.Units[0].transform.position.z);
    }

    void Update()
    {
        float yPos = transform.position.y;
        if (control)
        {
            yPos -= Controls.Instance.ScrollVal * Time.deltaTime * scrollSpeed * yConstant;
        }
        yPos = Mathf.Clamp(yPos, minCameraY, maxCameraY);

        float xPos = transform.position.x;
        float zPos = transform.position.z;
        if (control)
        {
            if (Controls.Instance.UpKey)
            {
                zPos += moveSpeed * Time.deltaTime * (yPos / yConstant);
            }
            if (Controls.Instance.DownKey)
            {
                zPos -= moveSpeed * Time.deltaTime * (yPos / yConstant);
            }
            if (Controls.Instance.LeftKey)
            {
                xPos -= moveSpeed * Time.deltaTime * (yPos / yConstant);
            }
            if (Controls.Instance.RightKey)
            {
                xPos += moveSpeed * Time.deltaTime * (yPos / yConstant);
            }
        }
        xPos = Mathf.Clamp(xPos, minCameraX, maxCameraX);
        zPos = Mathf.Clamp(zPos, minCameraZ, maxCameraZ);

        transform.position = new Vector3(xPos, yPos, zPos);
    }

    public float GetMinCameraY() 
    {
        return minCameraY;
    }
}
