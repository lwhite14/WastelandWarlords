 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public HexCell cellOn { get; private set; }

    public void Start()
    {
        FogOfWar.instance.CalculateVertexAlphas(transform.position, new Vector3(transform.position.x, transform.position.y + 200.0f, transform.position.z), 15.0f);
    }

    public void SetCell(HexCell newCell)
    {
        if (this.cellOn != null)
        {
            this.cellOn.collectable = null;
        }
        this.cellOn = newCell;
        this.cellOn.collectable = this;
        transform.SetParent(newCell.topTarget);
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
    }

    public void PickUp() 
    {
        if (!TutorialManager.instance.haveBattery) { TutorialManager.instance.haveBattery = true; TutorialManager.instance.CollectedBattery(); }
        Destroy(gameObject);
    }
}
