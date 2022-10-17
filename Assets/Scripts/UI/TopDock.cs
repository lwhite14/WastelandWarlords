using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopDock : MonoBehaviour
{
    public Text turnText;
    public Text moolahText;

    public void SetTurn(int turn) 
    {
        turnText.text = "Turn Number: " + turn.ToString();
    }

    public void SetMoolah(int moolah) 
    {
        moolahText.text = "Moolah: " + moolah.ToString();
    }
}
