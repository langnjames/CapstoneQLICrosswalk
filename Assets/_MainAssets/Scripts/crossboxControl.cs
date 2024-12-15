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

    TrafficManager  trafficController;

    

   // Start is called before the first frame update
   void Start()
    {
        // Gets refernce to traffic controller script for consistency throughout script.
        trafficController = TrafficManager.Instance;

    }

    // Update is called once per frame
    void Update()
    {

        float walktimer = trafficController.GetTimer();

        float timerVal = Mathf.Floor(walktimer);

        int intTimer = int.Parse(timerVal.ToString());

        //Debug.Log("WALKTIMER: " + walktimer + "\nFLOOR: " + timerVal + "\nINT: " + intTimer);

        if (intTimer <= 0)
        {
            walkTimerText.gameObject.SetActive(false);
            walkSign.gameObject.SetActive(false);
        }
        else
        {
            walkTimerText.SetText(timerVal.ToString());
            walkTimerText.gameObject.SetActive(true);
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

    
}
