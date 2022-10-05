using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float speed = 10.0f;

    void Update()
    {
        float xPos = transform.position.x;
        float zPos = transform.position.z;
        if (Input.GetKey(KeyCode.W)) 
        {
            zPos = transform.position.z + speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S)) 
        {
            zPos = transform.position.z - speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A)) 
        {
            xPos = transform.position.x - speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D)) 
        {
            xPos = transform.position.x + speed * Time.deltaTime;
        }
        //xPos = Mathf.Clamp(xPos, -20.0f, (HexMetrics.outerRadius * HexGrid.instance.width * 2) - 20.0f);
        //zPos = Mathf.Clamp(zPos, -20.0f, (HexMetrics.outerRadius * HexGrid.instance.height * 2) - 160.0f);
        transform.position = new Vector3(xPos, transform.position.y, zPos);
    }
}
