using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public static Controls Instance = null;

    public float ScrollVal { get; set; } = 0.0f;
    public bool LeftKey { get; set; } = false;
    public bool RightKey { get; set; } = false;
    public bool DownKey { get; set; } = false;
    public bool UpKey { get; set; } = false;
    public bool ClickLeft { get; set; } = false;
    public bool ClickRight { get; set; } = false;

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

    void Update()
    {
        ScrollVal = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetKey(KeyCode.W)) { UpKey = true; }
        else { UpKey = false; }

        if (Input.GetKey(KeyCode.S)) { DownKey = true; }
        else { DownKey = false; }

        if (Input.GetKey(KeyCode.A)) { LeftKey = true; }
        else { LeftKey = false; }

        if (Input.GetKey(KeyCode.D)) { RightKey = true; }
        else { RightKey = false; }

        if (Input.GetMouseButtonDown(0)) { ClickLeft = true; }
        else { ClickLeft = false; }

        if (Input.GetMouseButtonDown(1)) { ClickRight = true; }
        else { ClickRight = false; }
    }
}
