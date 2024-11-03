using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem.XR.Haptics;

public class StoplightScript : MonoBehaviour
{
    private GameObject[] stoplights;
    private GameObject[] redlights;
    private GameObject[] greenlights;
    private GameObject[] yellowlights;
    private GameObject[] crosswalks;

    private float lightSwapDuration = 2f;
    private bool isRedActive = true;

    private Stoplight lights = new Stoplight();

    public  Material activeGreenMat;
    public Material dimGreenMat;
    public Material activeRedMat;
    public Material dimRedMat;



    bool NSActive = true;
    Dictionary<int, Stoplight> stoplightDict = new();
    // Start is called before the first frame update
    void Start()
    {
        // Gets all stoplights in the scene
        stoplights = GameObject.FindGameObjectsWithTag("STOPLIGHT");
        crosswalks = GameObject.FindGameObjectsWithTag("CROSSWALK");

        Stoplight.SetMaterials(activeGreenMat, dimGreenMat, activeRedMat, dimRedMat);
       

        for (int i = 0; i < stoplights.Length; i++)
        {
            Stoplight lightObj = new Stoplight();
            string lightDirection = DetermineOrientation(stoplights[i]);
            lightObj.SetStopLight(stoplights[i], lightDirection);
            //Debug.Log(lightObj.stoplight.name +", Dir: " + lightDirection);

            stoplightDict.Add(i, lightObj);
        }

        
        StartCoroutine(SwapLights());
    }

    // Update is called once per frame
    void LightSwap()
    {
        

        //SetLights(lights.redLight, lights.greenLight, isRedActive);

    }

    void PairStoplights(GameObject[] stoplights)
    {
        foreach (GameObject stoplight in stoplights)
        {

        }
    }

    //void SetGreenLight(GameObject stoplight, bool active)
    //{ 
    //    stoplight.GetComponent<MeshRenderer>().material = active ? greenLightMatDim : greenLightMatActive;
    //}

    //void SetYellowLight(GameObject stoplight)
    //{
    //    stoplight.GetComponent <Material>().color = Color.yellow;
    //}

    //void SetRedLight(GameObject stoplight, bool active)
    //{
    //    stoplight.GetComponent<MeshRenderer>().material = active ? redLightMatDim : redLightMatActive;
    //}

    void SetLights()
    {
        if (NSActive)
        {
            foreach (Stoplight stoplight in stoplightDict.Values)
            {
                if (stoplight.direction == "N" || stoplight.direction == "S")
                {
                    stoplight.ActivateGo();
                }
                else if (stoplight.direction == "E" || stoplight.direction == "W")
                {
                    stoplight.ActivateStop();
                }
            }
            NSActive = false;
        }
        else
        {
            foreach (Stoplight stoplight in stoplightDict.Values)
            {
                if (stoplight.direction == "N" || stoplight.direction == "S")
                {
                    stoplight.ActivateStop();
                }
                else if (stoplight.direction == "E" || stoplight.direction == "W")
                {
                    stoplight.ActivateGo();

                }
            }
            NSActive = true;
        }

        SetCrosswalkSign(crosswalks);
    }

    void SetCrosswalkSign(GameObject[] crosswalks)
    {
        Stoplight light = stoplightDict.GetValueOrDefault(0);
        string lightState = Stoplight.GetState(light);
        Debug.Log(lightState);
        foreach (GameObject sign in crosswalks)
        {
            string signDirection = DetermineOrientation(sign);
            GameObject walkLight = sign.transform.Find("walkLight").gameObject;
            GameObject stopLight = sign.transform.Find("stopLight").gameObject;
            if (lightState == "NS")
            {
                if (signDirection == "N" || signDirection == "S")
                {
                    // Change mat to walk
                    walkLight.SetActive(true);
                    stopLight.SetActive(false);
                    
                }
                else // implies sign is 
                {
                    // Change mat to stop

                    walkLight.SetActive(false);
                    stopLight.SetActive(true);
                }
            }
            else // implies EW
            {
                if (signDirection == "E" || signDirection == "W")
                {
                    // Change mat to walk
                    walkLight.SetActive(true);
                    stopLight.SetActive(false);
                }
                else // implies sign is 
                {
                    // Change mat to stop
                    walkLight.SetActive(false);
                    stopLight.SetActive(true);
                }
            }
        }
    }

    public string DetermineOrientation(GameObject light)
    {
        string direction = "NA";

        // Get the Y rotation in degrees
        float yRotation = light.transform.eulerAngles.y;

        // Normalize the angle between 0 and 360 degrees
        yRotation = yRotation % 360f;

        // Define thresholds for each cardinal direction
        const float threshold = 45f; // Adjust as needed

        if (IsAngleWithinThreshold(yRotation, 0f, threshold) || IsAngleWithinThreshold(yRotation, 360f, threshold))
        {
            direction = "N";
            //Debug.Log("Light is facing North");
        }
        else if (IsAngleWithinThreshold(yRotation, 90f, threshold))
        {
            direction = "E";
            //Debug.Log("Light is facing East");
        }
        else if (IsAngleWithinThreshold(yRotation, 180f, threshold))
        {
            direction = "S";
            //Debug.Log("Light is facing South");
        }
        else if (IsAngleWithinThreshold(yRotation, 270f, threshold))
        {
            direction = "W";
            //Debug.Log("Light is facing West");
        }
        else
        {
            Debug.Log("Light is at an intermediate angle");
        }

        return direction;
    }

    private bool IsAngleWithinThreshold(float angle, float targetAngle, float threshold)
    {
        return Mathf.Abs(Mathf.DeltaAngle(angle, targetAngle)) <= threshold;
    }


    private IEnumerator SwapLights()
    {
        while (true)
        {
            SetLights();
            //Debug.Log("Swapped");
            yield return new WaitForSeconds(lightSwapDuration);
        }
        
    }

}
