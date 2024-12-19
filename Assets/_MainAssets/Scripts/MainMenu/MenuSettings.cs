using UnityEngine;
using TMPro;

public class MenuSettings : MonoBehaviour
{
    // Singleton instance
    public static MenuSettings Instance { get; private set; }

    // Settings variables
    public float walkTimer = 10f;  //-1 will be considered disabled/infinite time, otherwise # is seconds
    public string levelSelect = "Level One"; //string relating to scene name
    public int trafficLevel = 1; //0 is none, then range from 1-3

    private void Awake()
    {
        // Ensure only one instance of this object exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist across scenes
    }

    public void setValues() //only call from main menu
    {
        string TimerTxt = GameObject.Find("TimerSelectLabel").GetComponent<TMP_Text>().text;
        string TrafficTxt = GameObject.Find("TrafficSelectLabel").GetComponent<TMP_Text>().text;
        string LevelText = GameObject.Find("LevelSelectLabel").GetComponent<TMP_Text>().text;
        SetTimer(TimerTxt);
        SetTraffic(TrafficTxt);
        SetLevel(LevelText);
    }

    public void SetTimer(string time)
    {
        if (time == "Unlimited")
        {
            walkTimer = 10000;
        }
        else 
        {
            string[] parts = time.Split(' ');
            float.TryParse(parts[0], out walkTimer);
            //Debug.Log("walk timer supposed: " + walkTimer);
        }
        //Debug.Log("Time set to: " + walkTimer);
    }

    public void SetTraffic(string traffic)
    {
        switch (traffic)
        {
            case "None":
                trafficLevel = 0; break;
            case "Low":
                trafficLevel = 1; break;
            case "Medium":
                trafficLevel = 2; break;
            case "High":
                trafficLevel = 3; break;
        }
        //Debug.Log("traffic level set to: " + trafficLevel);
    }

    public void SetLevel(string level)
    {
        levelSelect = level;
        //Debug.Log("level set to: " + levelSelect);
    }
}
