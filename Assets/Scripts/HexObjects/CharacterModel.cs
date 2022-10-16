using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : MonoBehaviour
{

    public void PlayAttackSound()
    {
        transform.parent.GetComponent<AudioSource>().Stop();
        GameObject tempObj = Instantiate<GameObject>(ResourceFactory.Punch);
        tempObj.transform.position = transform.position;
    }
}
