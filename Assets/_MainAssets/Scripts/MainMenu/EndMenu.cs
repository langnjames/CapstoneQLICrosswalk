using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void ResetGame()
    {
        string sceneToLoad = "MainMenuScene";
        DeleteAllGO();
        SceneManager.LoadScene(sceneToLoad);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void DeleteAllGO()
    {
        Object[] allObjects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject obj in allObjects)
        {
            Destroy(obj);
        }

    }
}
