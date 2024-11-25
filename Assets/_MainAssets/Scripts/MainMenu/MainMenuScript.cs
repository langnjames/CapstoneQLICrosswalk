using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class MainMenuScript : MonoBehaviour
{
    public GameObject menu;
    public string[] timerOptions = { "Unlimited", "10 Seconds", "20 Seconds", "40 Seconds", "60 Seconds", "80 Seconds", "99 Seconds" };
    public string[] trafficOptions = { "None", "Low", "Medium", "High" };
    public string[] levelOptions = { "Level One", "Level Two" };
    public string levelToSet;

    public void StartGame()
    {
        if (menu != null) 
        { 
            Destroy(menu);
        }
        LoadScene(levelToSet);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string scene)
    {
        string sceneToLoad = "Street1"; // Default value to set is scene 1
        switch (scene) 
        {
            case "Level One":
                sceneToLoad = "Street1"; break;
            case "Level Two":
                sceneToLoad = "Street2"; break;
        }
        SceneManager.LoadScene(sceneToLoad);
    }

    public void TimerIncrement(int direction)
    {

        TMP_Text label = GameObject.Find("TimerSelectLabel").GetComponent<TMP_Text>();
        int newPosition = System.Array.IndexOf(timerOptions, label.text) + direction;
        if (newPosition < 0)
        {
            newPosition = timerOptions.Length-1;
        }
        else if (newPosition >= timerOptions.Length)
        {
            newPosition = 0;
        }
        label.text = timerOptions[newPosition];
    }
    public void TrafficIncrement(int direction)
    {
        TMP_Text label = GameObject.Find("TrafficSelectLabel").GetComponent<TMP_Text>();
        int newPosition = System.Array.IndexOf(trafficOptions, label.text) + direction;
        if (newPosition < 0)
        {
            newPosition = trafficOptions.Length-1;
        }
        else if (newPosition >= trafficOptions.Length)
        {
            newPosition = 0;
        }
        label.text = trafficOptions[newPosition];
    }
    public void LevelIncrement(int direction)
    {
        TMP_Text label = GameObject.Find("LevelSelectLabel").GetComponent<TMP_Text>();
        int newPosition = System.Array.IndexOf(levelOptions, label.text) + direction;
        if (newPosition < 0)
        {
            newPosition = levelOptions.Length-1;
        }
        else if (newPosition >= levelOptions.Length)
        {
            newPosition = 0;
        }
        label.text = levelOptions[newPosition];
        levelToSet = levelOptions[newPosition];
    }
}
