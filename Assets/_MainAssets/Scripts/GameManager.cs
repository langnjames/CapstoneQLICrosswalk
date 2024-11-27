using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public FadeCanvas fadeCanvas;

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
        fadeCanvas.StartFadeOut();
        Invoke("CurrentSceneReset", 3f);
    }

    public void CurrentSceneReset()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log("Current Scene: " + currentScene);
        SceneManager.LoadScene(currentScene);
    }

    public void ResetScene(string message)
    {
        ResetScene();
        // Send UI Panel Message and activate the panel

    }

    public void EndGame()
    {
        Debug.Log("Called game from gamemanager");
        Application.Quit();
        SceneManager.LoadScene("EndScene");
    }
}
