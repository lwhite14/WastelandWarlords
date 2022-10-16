using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;

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

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            QuitGame();
        }
    }

    public void StartGame() 
    {
        GameState.ResetFields();
        SceneManager.LoadScene("Level1");
    }

    public void ReturnToMenu() 
    {
        GameState.ResetFields();
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

}
