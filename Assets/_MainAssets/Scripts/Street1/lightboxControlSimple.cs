using System.Collections;
using System.Collections.Generic;
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
    
    // The object to look at for instructions
    //private trafficManagerSimple TrafficManager;

    // Start is called before the first frame update
    void Start()
    {
        // Get the traffic manager object's component
        //TrafficManager = GameObject.FindWithTag("TrafficManager").GetComponent<trafficManagerSimple>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(trafficManagerSimple.Instance.getStatus() == "go") //cars go
        {
            if(!UseWalkboxOnly)
            {
                RedLightObj.GetComponent<MeshRenderer>().material = RedOff; // red
                YellowLightObj.GetComponent<MeshRenderer>().material = YellowOff; // yellow
                GreenLightObj.GetComponent<MeshRenderer>().material = GreenOn; // green
            }

            WalkBox.active = false; //walkBox
            StopBox.active = true; //stopBox 
        }
        else if(trafficManagerSimple.Instance.getStatus() == "slow") //cars slow
        {
            if(!UseWalkboxOnly)
            {
                RedLightObj.GetComponent<MeshRenderer>().material = RedOff; // red
                YellowLightObj.GetComponent<MeshRenderer>().material = YellowOn; // yellow
                GreenLightObj.GetComponent<MeshRenderer>().material = GreenOff; // green
            }

            WalkBox.active = false; //walkBox
            StopBox.active = true; //stopBox 
        }
        else //assume stop
        {
            if(!UseWalkboxOnly)
            {
                RedLightObj.GetComponent<MeshRenderer>().material = RedOn; // red
                YellowLightObj.GetComponent<MeshRenderer>().material = YellowOff; // yellow
                GreenLightObj.GetComponent<MeshRenderer>().material = GreenOff; // green
            }
            WalkBox.active = true; //walkBox
            StopBox.active = false; //stopBox 
        }
    }
}
