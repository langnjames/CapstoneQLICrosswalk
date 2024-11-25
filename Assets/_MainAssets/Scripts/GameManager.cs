using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState
    {
        Menu,
        Playing,
        GameOver
    }

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ResetScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log("Current Scene: " + currentScene.ToString());
        SceneManager.LoadScene(currentScene.ToString());
    }

    public void EndGame()
    {
        Debug.Log("Called game from gamemanager");
        Application.Quit();
        SceneManager.LoadScene("EndScene");
    }
}
