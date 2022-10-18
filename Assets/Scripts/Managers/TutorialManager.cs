using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance = null;

    public Text TextBox;
    bool firstEnemySpotted = false;
    bool firstReturnBattery = false;
    bool firstUnitDied = false;

    public float panSpeed = 4.0f;
    public float letterAppearSpeed = 0.05f;
    public float nextSentenceSpeed = 0.05f;

    public bool haveBattery = false;

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
        StopAllCoroutines();
        StartCoroutine(PreMove());
    }

    IEnumerator PreMove()
    {
        yield return StartCoroutine(SpottingFirstSettlement(GameState.Settlements[0]));
        yield return StartCoroutine(SpottingFirstUnit(GameState.Units[0]));
        yield return StartCoroutine(SpottingBattery(GameState.Collectables[0]));
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

        if (haveBattery && !firstReturnBattery) 
        {
            if (GameState.Units[0].cellOn == GameState.Settlements[0].cellOn) 
            {
                firstReturnBattery = true;
                StopAllCoroutines();
                StartCoroutine(ReturnedWithBattery());
            }
        }
    }

    public void CollectedBattery() 
    {
        StopAllCoroutines();
        StartCoroutine(CollectBatteryNotification());
    }

    public void UnitDied() 
    {
        StopAllCoroutines();
        StartCoroutine(UnitDiedNotification());
    }

    IEnumerator CollectBatteryNotification() 
    {
        yield return StartCoroutine(DisplayConversation(new string[] { "Wow thanks, I didn't think you would actually make it!", "Just come on home to Grapguard now..." }));
    }
    IEnumerator ReturnedWithBattery()
    {
        yield return StartCoroutine(DisplayConversation(new string[] { "Wow well done!", "I really didn't think you would make it home.", "A successful mission. You felled " + GameStatistics.EnemiesFelled.ToString() + " enemies, traversed " + GameStatistics.HexesTraversed.ToString() + " hexes, all in " + GameStatistics.TurnNumber.ToString() + " turns!" }));
        LevelManager.Instance.ReturnToMenu();
        yield return null;
    }

    IEnumerator UnitDiedNotification() 
    {
        yield return StartCoroutine(DisplayConversation(new string[] { "Oh no... Lucian has perished...", "On other levels you will be able to recruit more units, but for now you just have Lucian...", "...or you had Lucian...", "Let's have another go!" }));
        LevelManager.Instance.StartGame();
        yield return null;
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
        CameraControls.Instance.control = false;
        HexGrid.Instance.canClick = false;

        Vector3 startPosition = CameraControls.Instance.transform.position;
        Vector3 endPosition = new Vector3(enemy.transform.position.x, CameraControls.Instance.GetMinCameraY(), enemy.transform.position.z - 20.0f);

        yield return LerpToPoint(CameraControls.Instance.transform, startPosition, endPosition);
        yield return StartCoroutine(DisplayConversation(new string[] {"He's just standing there... menacingly...", "You can attack him, or go around him, but don't get too close before you end your turn..." }));

        startPosition = CameraControls.Instance.transform.position;
        endPosition = new Vector3(CameraControls.Instance.transform.position.x, CameraControls.Instance.transform.position.y + 20.0f, CameraControls.Instance.transform.position.z);

        yield return LerpToPoint(CameraControls.Instance.transform, startPosition, endPosition);

        CameraControls.Instance.control = true;
        HexGrid.Instance.canClick = true;
        yield return null;
    }

    IEnumerator SpottingFirstSettlement(Settlement settlement) 
    {
        CameraControls.Instance.control = false;
        HexGrid.Instance.canClick = false;

        Vector3 startPosition = CameraControls.Instance.transform.position;
        Vector3 endPosition = new Vector3(settlement.transform.position.x, CameraControls.Instance.GetMinCameraY(), settlement.transform.position.z - 20.0f);

        yield return LerpToPoint(CameraControls.Instance.transform, startPosition, endPosition);
        yield return StartCoroutine(DisplayConversation(new string[] { "Your home, Grapguard.", "Here you are safe from the monsters who patrol the wasteland... relatively safe..." }));
        
        startPosition = CameraControls.Instance.transform.position;
        endPosition = new Vector3(CameraControls.Instance.transform.position.x, CameraControls.Instance.transform.position.y + 20.0f, CameraControls.Instance.transform.position.z);

        yield return LerpToPoint(CameraControls.Instance.transform, startPosition, endPosition);

        CameraControls.Instance.control = true;
        HexGrid.Instance.canClick = true;
        yield return null;
    }

    IEnumerator SpottingFirstUnit(Unit unit)
    {
        CameraControls.Instance.control = false;
        HexGrid.Instance.canClick = false;

        Vector3 startPosition = CameraControls.Instance.transform.position;
        Vector3 endPosition = new Vector3(unit.transform.position.x, CameraControls.Instance.GetMinCameraY(), unit.transform.position.z - 20.0f);

        yield return LerpToPoint(CameraControls.Instance.transform, startPosition, endPosition);
        yield return StartCoroutine(DisplayConversation(new string[] { "Your faithful servant, Lucian.", "He will carry out your will, as long as you keep him safe...", "Left-click him to display his movement range, right-click to move him!", "You can get more movement points for Lucian by ending your turn..." }));

        startPosition = CameraControls.Instance.transform.position;
        endPosition = new Vector3(CameraControls.Instance.transform.position.x, CameraControls.Instance.transform.position.y + 20.0f, CameraControls.Instance.transform.position.z);

        yield return LerpToPoint(CameraControls.Instance.transform, startPosition, endPosition);

        CameraControls.Instance.control = true;
        HexGrid.Instance.canClick = true;
        yield return null;
    }

    IEnumerator SpottingBattery(Collectable battery)
    {
        CameraControls.Instance.control = false;
        HexGrid.Instance.canClick = false;

        Vector3 startPosition = CameraControls.Instance.transform.position;
        Vector3 endPosition = new Vector3(battery.transform.position.x, CameraControls.Instance.GetMinCameraY(), battery.transform.position.z - 20.0f);

        yield return LerpToPoint(CameraControls.Instance.transform, startPosition, endPosition);
        yield return StartCoroutine(DisplayConversation(new string[] { "We need some batteries... do you mind popping out to grab some? Thanks." }));

        startPosition = CameraControls.Instance.transform.position;
        endPosition = new Vector3(GameState.Units[0].transform.position.x, CameraControls.Instance.transform.position.y + 20.0f, GameState.Units[0].transform.position.z - 20.0f);

        yield return LerpToPoint(CameraControls.Instance.transform, startPosition, endPosition);

        CameraControls.Instance.control = true;
        HexGrid.Instance.canClick = true;
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
