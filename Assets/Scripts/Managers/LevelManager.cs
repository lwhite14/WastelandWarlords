using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance = null;

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
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            QuitGame();
        }
    }

    public void StartGame() 
    {
        ResetAllFields();
        SceneManager.LoadScene("Level1");
    }

    public void ReturnToMenu() 
    {
        ResetAllFields();
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    void ResetAllFields() 
    {
        GameState.ResetFields();
        GameStatistics.ResetFields();
        ClickMode.ResetFields();
    }
}
