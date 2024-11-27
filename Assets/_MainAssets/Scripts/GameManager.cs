using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public FadeCanvas fadeCanvas;
    public GameObject player;

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
        //DontDestroyOnLoad(gameObject);
    }

    public void ResetScene()
    {
        fadeCanvas.StartFadeIn();
        Invoke("CurrentSceneReset", 5f);
    }

    public void CurrentSceneReset()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log("Current Scene: " + currentScene);
        fadeCanvas.StartFadeOut();
        SceneManager.LoadScene(currentScene);
    }

    public void ResetScene(string message)
    {
        ResetScene();
        player.GetComponentInChildren<InputActionManager>().enabled = false;
        fadeCanvas.SetText("Try Again. " + message);

    }

    public void EndGame()
    {
        Debug.Log("Called game from gamemanager");
        Application.Quit();
        SceneManager.LoadScene("EndScene");
    }
}
