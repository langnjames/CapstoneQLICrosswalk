using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightboxControl : MonoBehaviour
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

    //[Header("The Walk Box Objects")]
    //public bool UseWalkboxOnly = false;
    //public GameObject WalkBox;
    //public GameObject StopBox;

    private TrafficManager trafficManager;

    [Header("The Traffic Light Direction")]
    public direction stoplightDirection;

    public enum direction
    {
        EW,
        NS
    }

    //String for comparing states
    private string lightDirection;

    

    // The object to look at for instructions
    //private trafficManagerSimple TrafficManager;

    // Start is called before the first frame update
    void Start()
    {
        // Get the traffic manager object's component
        trafficManager = TrafficManager.Instance;

        lightDirection = stoplightDirection.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        string activeDirection = trafficManager.activeDirection.ToString();
        

        //Debug.Log(activeDirection + " " + stoplightDirection);

        if (activeDirection == lightDirection)
        {
            if (trafficManager.GetStatus() == "go") //cars go
            {
                RedLightObj.GetComponent<MeshRenderer>().material = RedOff; // red
                YellowLightObj.GetComponent<MeshRenderer>().material = YellowOff; // yellow
                GreenLightObj.GetComponent<MeshRenderer>().material = GreenOn; // green
            }
            else if (trafficManager.GetStatus() == "slow") //cars slow
            {
                RedLightObj.GetComponent<MeshRenderer>().material = RedOff; // red
                YellowLightObj.GetComponent<MeshRenderer>().material = YellowOn; // yellow
                GreenLightObj.GetComponent<MeshRenderer>().material = GreenOff; // green
            }
        }
        else
        {
                RedLightObj.GetComponent<MeshRenderer>().material = RedOn; // red
                YellowLightObj.GetComponent<MeshRenderer>().material = YellowOff; // yellow
                GreenLightObj.GetComponent<MeshRenderer>().material = GreenOff; // green   
        }

        //if (trafficManagerSimple.Instance.getStatus() == "go") //cars go
        //{

        //        RedLightObj.GetComponent<MeshRenderer>().material = RedOff; // red
        //        YellowLightObj.GetComponent<MeshRenderer>().material = YellowOff; // yellow
        //        GreenLightObj.GetComponent<MeshRenderer>().material = GreenOn; // green


        //    //WalkBox.active = false; //walkBox
        //    //StopBox.active = true; //stopBox 
        //}
        //else if (trafficManagerSimple.Instance.getStatus() == "slow") //cars slow
        //{

        //        RedLightObj.GetComponent<MeshRenderer>().material = RedOff; // red
        //        YellowLightObj.GetComponent<MeshRenderer>().material = YellowOn; // yellow
        //        GreenLightObj.GetComponent<MeshRenderer>().material = GreenOff; // green



        //}
        //else //assume stop
        //{

        //        RedLightObj.GetComponent<MeshRenderer>().material = RedOn; // red
        //        YellowLightObj.GetComponent<MeshRenderer>().material = YellowOff; // yellow
        //        GreenLightObj.GetComponent<MeshRenderer>().material = GreenOff; // green

        //}
    }
}
