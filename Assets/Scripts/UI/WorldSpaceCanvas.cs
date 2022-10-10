using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceCanvas : MonoBehaviour
{
    void Awake() 
    {
        GetComponent<Canvas>().worldCamera = GameObject.Find("Camera").GetComponent<Camera>();
    }
}
