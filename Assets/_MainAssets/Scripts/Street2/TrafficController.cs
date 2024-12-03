using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class TrafficController : MonoBehaviour
{
    public static TrafficController Instance {  get; private set; }

    private StoplightScript stoplights;
    private Stoplight stoplight;

    private Stoplight NSStoplight;
    private Stoplight EWStoplight;


    // Lights for status
    private int status = 0;

    private float walkTimer = 0;
    private bool walkTriggered = false;

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

    private void Start()
    {
        Time.fixedDeltaTime = 1 / 60f; // 60 fps update loop
        GetDirectionalLights();
        walkTimer = MenuSettings.Instance.walkTimer;
    }




    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("Green Direction: " + StoplightScript.Instance.currentActiveDirection);
        //Debug.Log("EW Stoplight Green: " + EWStoplight.IsGreen());
        

        if (StoplightScript.Instance.currentActiveDirection == StoplightScript.ActiveDirection.EW)
        {
            status = 0;
        }
        else if (StoplightScript.Instance.currentActiveDirection == StoplightScript.ActiveDirection.NS)
        {
            status = 1;
        }

        if (walkTriggered)
        {
            FailureTimer();
        }

    }

    void FailureTimer()
    {
        walkTimer -= Time.fixedDeltaTime;
        if (walkTimer <= 0)
        {
            walkTriggered = false;
            GameManager.Instance.ResetScene("You failed to cross the crosswalk in time"); // This can be anything but I decided to end the game when person didn't complete it
        }
    }



    public void TriggerWalk()
    {
        StoplightScript.Instance.TriggerWalk();
        walkTriggered = true;
    }

    private void GetDirectionalLights()
    {
        for (int i = 0; i < StoplightScript.Instance.stoplightDict.Count; i++)
        {
            if (NSStoplight != null && EWStoplight != null)
            {
                break;
            }
            else if (StoplightScript.Instance.stoplightDict[i].GetDirection() == "N" || StoplightScript.Instance.stoplightDict[i].GetDirection() == "S")
            {
                NSStoplight = StoplightScript.Instance.stoplightDict[i];
            }
            else
            {
                EWStoplight = StoplightScript.Instance.stoplightDict[i];
            }
        }
    }

    public string GetStatus()
    {
        switch (status) 
        {
            default:
                return "Stop";
            case 0:
                return "NS Stop";
            case 1:
                return "EW Stop";
        }
    }
}
