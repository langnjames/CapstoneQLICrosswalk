using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class lightboxControlSimple : MonoBehaviour
{
    
    [Header("The Traffic Lights")]
    public GameObject RedLightObj;
    public GameObject YellowLightObj;
    public GameObject GreenLightObj;
    
    [Header("The Traffic Light Materials")]
    public Material RedOn;
    public Material RedOff;

    public Material YellowOn;
    public Material YellowOff;

    public Material GreenOn;
    public Material GreenOff;

    [Header("The Walk Box Objects")]
    public bool UseWalkboxOnly = false;
    public GameObject WalkBox;
    public GameObject StopBox;
    public TMP_Text walkTimerText;

    // Materials to use
    Material signMaterial;
    Material blackMaterial;
    
    // The object to look at for instructions
    //private trafficManagerSimple TrafficManager;

    // Start is called before the first frame update
    void Start()
    {
        // Get the traffic manager object's component
        //TrafficManager = GameObject.FindWithTag("TrafficManager").GetComponent<trafficManagerSimple>(); 
        if(walkTimerText == null)
        {
            walkTimerText.SetText("");
        }
       
    }

    // Update is called once per frame
    void Update()
    {

        float walktimer = trafficManagerSimple.Instance.GetTimer();

        float timerVal = Mathf.Floor(walktimer);

        int intTimer = int.Parse(timerVal.ToString());

        if (intTimer <= 0)
        {
            walkTimerText.gameObject.SetActive(false);
            WalkBox.gameObject.SetActive(false);
        }
        else
        {
            walkTimerText.SetText(timerVal.ToString());
            walkTimerText.gameObject.SetActive(true);
            if (intTimer > 0 && intTimer <= 10)
            {
                WalkBox.gameObject.SetActive(false);
                StopBox.gameObject.SetActive(true);
            }
            else
            {
               WalkBox.gameObject.SetActive(true);
            }
        }

        if (trafficManagerSimple.Instance.getStatus() == "go") //cars go
        {
            if(!UseWalkboxOnly)
            {
                RedLightObj.GetComponent<MeshRenderer>().material = RedOff; // red
                YellowLightObj.GetComponent<MeshRenderer>().material = YellowOff; // yellow
                GreenLightObj.GetComponent<MeshRenderer>().material = GreenOn; // green
            }
        }
        else if(trafficManagerSimple.Instance.getStatus() == "slow") //cars slow
        {
            if(!UseWalkboxOnly)
            {
                RedLightObj.GetComponent<MeshRenderer>().material = RedOff; // red
                YellowLightObj.GetComponent<MeshRenderer>().material = YellowOn; // yellow
                GreenLightObj.GetComponent<MeshRenderer>().material = GreenOff; // green
            }

        }
        else //assume stop
        {
            if(!UseWalkboxOnly)
            {
                RedLightObj.GetComponent<MeshRenderer>().material = RedOn; // red
                YellowLightObj.GetComponent<MeshRenderer>().material = YellowOff; // yellow
                GreenLightObj.GetComponent<MeshRenderer>().material = GreenOff; // green
            }
        }
    }
}
