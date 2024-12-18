using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class crossboxControl : MonoBehaviour
{
    [Header("The Traffic Light Direction")]
    public direction stoplightDirection;

    public enum direction
    {
        EW,
        NS
    }

    [Header("Light Objects")]
    public GameObject walkSign;
    public GameObject stopSign;
    public TMP_Text walkTimerText;

    TrafficManager  trafficManager;

    private string lightDirection;

    

   // Start is called before the first frame update
   void Start()
    {
        // Gets refernce to traffic controller script for consistency throughout script.
        trafficManager = TrafficManager.Instance;

        lightDirection = stoplightDirection.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        string activeDirection = trafficManager.activeDirection.ToString();
        
        float walktimer = trafficManager.GetTimer();

        float timerVal = Mathf.Floor(walktimer);

        int intTimer = int.Parse(timerVal.ToString());

        //Debug.Log("WALKTIMER: " + walktimer + "\nFLOOR: " + timerVal + "\nINT: " + intTimer);

        if (activeDirection == lightDirection)
        {
            if (intTimer <= 0)
            {
                walkTimerText.SetText("");
                walkTimerText.gameObject.SetActive(false);
                walkSign.gameObject.SetActive(false);
            }
            else
            {
                walkTimerText.gameObject.SetActive(true);
                if (intTimer > 0 && intTimer < 100)
                {
                    walkTimerText.SetText(timerVal.ToString());
                }
                
                if (intTimer > 0 && intTimer <= 10)
                {
                    walkSign.gameObject.SetActive(false);
                    stopSign.gameObject.SetActive(true);
                }
                else
                {
                    walkSign.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            walkSign.gameObject.SetActive(false);
            stopSign.gameObject.SetActive(true);
        }

       


    }

    
}
