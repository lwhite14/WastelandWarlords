using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TutorialManager : MonoBehaviour
{
    public Text TextBox;
    bool firstEnemySpotted = false;

    public float panSpeed = 4.0f;
    public float letterAppearSpeed = 0.05f;
    public float nextSentenceSpeed = 0.05f;

    void Start()
    {
        StopAllCoroutines();
        StartCoroutine(PreMove());
    }

    IEnumerator PreMove() 
    {
        yield return StartCoroutine(SpottingFirstSettlement(GameState.Settlements[0]));
        yield return StartCoroutine(SpottingFirstUnit(GameState.Units[0]));
        yield return null;
    }

    void Update()
    {
        if (!firstEnemySpotted)
        {
            Enemy enemyForCoroutine = null;
            foreach (Enemy enemy in GameState.Enemies)
            {
                if (enemy.canBeSeen)
                {
                    firstEnemySpotted = true;
                    enemyForCoroutine = enemy;
                    break;
                }
            }
            if (firstEnemySpotted)
            {
                StopAllCoroutines();
                StartCoroutine(SpottingFirstEnemy(enemyForCoroutine));
            }
        }
    }

    IEnumerator LerpToPoint(Transform toChange, Vector3 startPos, Vector3 endPos) 
    {
        float progress = 0.0f;
        while (progress <= 1.0f)
        {
            toChange.position = Vector3.Lerp(startPos, endPos, progress);
            progress += Time.deltaTime * panSpeed;
            yield return null;
        }
        toChange.position = endPos;
        yield return null;
    }

    IEnumerator SpottingFirstEnemy(Enemy enemy) 
    {
        CameraControls.instance.control = false;

        Vector3 startPosition = CameraControls.instance.transform.position;
        Vector3 endPosition = new Vector3(enemy.transform.position.x, CameraControls.instance.GetMinCameraY(), enemy.transform.position.z - 20.0f);

        yield return LerpToPoint(CameraControls.instance.transform, startPosition, endPosition);
        yield return StartCoroutine(DisplayConversation(new string[] {"He's just standing there... menacingly...", "You can attack him, or go around him, but don't get too close before you end your turn..." }));

        startPosition = CameraControls.instance.transform.position;
        endPosition = new Vector3(CameraControls.instance.transform.position.x, CameraControls.instance.transform.position.y + 20.0f, CameraControls.instance.transform.position.z);

        yield return LerpToPoint(CameraControls.instance.transform, startPosition, endPosition);

        CameraControls.instance.control = true;
        yield return null;
    }

    IEnumerator SpottingFirstSettlement(Settlement settlement) 
    {
        CameraControls.instance.control = false;
        Vector3 startPosition = CameraControls.instance.transform.position;
        Vector3 endPosition = new Vector3(settlement.transform.position.x, CameraControls.instance.GetMinCameraY(), settlement.transform.position.z - 20.0f);

        yield return LerpToPoint(CameraControls.instance.transform, startPosition, endPosition);
        yield return StartCoroutine(DisplayConversation(new string[] { "Your home, Grapguard.", "Here you are safe from the monsters who pratrol the wasteland... relatively safe..." }));
        
        startPosition = CameraControls.instance.transform.position;
        endPosition = new Vector3(CameraControls.instance.transform.position.x, CameraControls.instance.transform.position.y + 20.0f, CameraControls.instance.transform.position.z);

        yield return LerpToPoint(CameraControls.instance.transform, startPosition, endPosition);

        CameraControls.instance.control = true;
        yield return null;
    }

    IEnumerator SpottingFirstUnit(Unit unit)
    {
        CameraControls.instance.control = false;
        Vector3 startPosition = CameraControls.instance.transform.position;
        Vector3 endPosition = new Vector3(unit.transform.position.x, CameraControls.instance.GetMinCameraY(), unit.transform.position.z - 20.0f);

        yield return LerpToPoint(CameraControls.instance.transform, startPosition, endPosition);
        yield return StartCoroutine(DisplayConversation(new string[] { "Your faithful servant, Lucian.", "He will carry out your will, as long as you keep him safe...", "Left-click him to display his movement range, right-click to move him!" }));

        startPosition = CameraControls.instance.transform.position;
        endPosition = new Vector3(CameraControls.instance.transform.position.x, CameraControls.instance.transform.position.y + 20.0f, CameraControls.instance.transform.position.z);

        yield return LerpToPoint(CameraControls.instance.transform, startPosition, endPosition);

        CameraControls.instance.control = true;
        yield return null;
    }

    IEnumerator DisplayConversation(string[] conversion) 
    {
        foreach (string sentence in conversion) 
        {
            yield return StartCoroutine(DisplayText(sentence));
        }
        yield return null;
    }

    IEnumerator DisplayText(string text) 
    {
        int length = text.Length;
        for (int i = 0; i < length; i++) 
        {
            TextBox.text += text[i];
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1.5f);
        TextBox.text = "";
        yield return null;
    }
}
