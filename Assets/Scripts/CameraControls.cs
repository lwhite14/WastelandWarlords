using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraControls : MonoBehaviour
{
    public static CameraControls instance = null;

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
        //transform.position = new Vector3(minCameraX + ((maxCameraX - minCameraX) / 2), transform.position.y, minCameraZ + ((maxCameraZ - minCameraZ) / 2));
        transform.position = new Vector3(GameState.Units[0].transform.position.x, transform.position.y, GameState.Units[0].transform.position.z);
    }

    void Update()
    {
        float yPos = transform.position.y;
        if (control)
        {
            yPos -= Controls.instance.ScrollVal * Time.deltaTime * scrollSpeed * yConstant;
        }
        yPos = Mathf.Clamp(yPos, minCameraY, maxCameraY);

        float xPos = transform.position.x;
        float zPos = transform.position.z;
        if (control)
        {
            if (Controls.instance.UpKey)
            {
                zPos += moveSpeed * Time.deltaTime * (yPos / yConstant);
            }
            if (Controls.instance.DownKey)
            {
                zPos -= moveSpeed * Time.deltaTime * (yPos / yConstant);
            }
            if (Controls.instance.LeftKey)
            {
                xPos -= moveSpeed * Time.deltaTime * (yPos / yConstant);
            }
            if (Controls.instance.RightKey)
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
