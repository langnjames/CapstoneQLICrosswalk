using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.XR.Interaction.Toolkit;

public class StoplightScript : MonoBehaviour
{
    private GameObject[] stoplights;
    private GameObject[] redlights;
    private GameObject[] greenlights;
    private GameObject[] yellowlights;
    private GameObject[] crosswalks;

    
    private bool isRedActive = true;

    private Stoplight lights = new Stoplight();

    public  Material activeGreenMat;
    public Material dimGreenMat;
    public Material activeRedMat;
    public Material dimRedMat;
    public Material activeYellowMat;
    public Material dimYellowMat;

    private float greenDuration = 10f;
    private float yellowDuration = 2f;
    private float redDuration = 10f;
    private float lightSwapDuration; // 22f



    public bool NSActive = true;
    public bool EWActive = false;
    public Dictionary<int, Stoplight> stoplightDict = new();

    /* SCENE DEFAULTS */
    float defaultWalkTimer = 10f;

    public enum ActiveDirection
    {
        NS,
        EW,
        None
    }

    public ActiveDirection currentActiveDirection = ActiveDirection.NS;

    public static StoplightScript Instance {  get; private set; }
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

        // Gets all stoplights in the scene
        stoplights = GameObject.FindGameObjectsWithTag("STOPLIGHT");
        crosswalks = GameObject.FindGameObjectsWithTag("CROSSWALK");

        Stoplight.SetMaterials(activeGreenMat, dimGreenMat, activeRedMat, dimRedMat, activeYellowMat, dimYellowMat);


        for (int i = 0; i < stoplights.Length; i++)
        {
            Stoplight lightObj = new Stoplight();
            string lightDirection = DetermineOrientation(stoplights[i]);
            lightObj.SetStopLight(stoplights[i], lightDirection);
            //Debug.Log(lightObj.stoplight.name +", Dir: " + lightDirection);

            stoplightDict.Add(i, lightObj);
        }

        
    }

    void Start()
    {
        CalculateLightCycles();
        StartCoroutine(SwapLights());
    }

    void SetLights()
    {
        if (NSActive)
        {
            
            foreach (Stoplight stoplight in stoplightDict.Values)
            {
                if (stoplight.direction == "N" || stoplight.direction == "S")
                {
                    //Debug.Log("Got here");
                    StartTrafficCycle(stoplight);
                }
                else if (stoplight.direction == "E" || stoplight.direction == "W")
                {
                    //stoplight.ActivateStop();
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
                    //stoplight.ActivateStop();
                }
                else if (stoplight.direction == "E" || stoplight.direction == "W")
                {
                    StartTrafficCycle(stoplight);
                    

                }
            }
            NSActive = true;
        }

        SetCrosswalkSign(crosswalks);
    }

    public void ActivateGo(Stoplight stoplight)
    {
        if (currentActiveDirection == ActiveDirection.NS)
        {
            currentActiveDirection = ActiveDirection.EW;
        }
        else
        {
            currentActiveDirection = ActiveDirection.NS;
        }

        stoplight.ActivateGo();

        
    }

    public void ActivateYield(Stoplight stoplight)
    {
        currentActiveDirection = ActiveDirection.None;
        stoplight.ActivateYield();
    }

    public void ActivateStop(Stoplight stoplight)
    {
        stoplight.ActivateStop();
    }

    public void TriggerWalk()
    {
        StopAllCoroutines(); 
    }

    public void CalculateLightCycles()
    {
        //Init
        float walkTimer;

        // Confirm wether Menu scene was initialized or not
        if (MenuSettings.Instance == null)
        {
            walkTimer = defaultWalkTimer;
        }
        else
        {
            walkTimer = MenuSettings.Instance.walkTimer;
        }

        Debug.Log("walkTimer: " + walkTimer);
        
        greenDuration = walkTimer - yellowDuration;
        redDuration = greenDuration;
        lightSwapDuration = greenDuration + yellowDuration;

    }

    void SetCrosswalkSign(GameObject[] crosswalks)
    {
        Stoplight light = stoplightDict.GetValueOrDefault(0);
        string lightState = light.GetState();
        foreach (GameObject sign in crosswalks)
        {
            string signDirection = DetermineOrientation(sign);
            GameObject walkLight = sign.transform.Find("walkLight").gameObject;
            GameObject stopLight = sign.transform.Find("stopLight").gameObject;
            if (currentActiveDirection == ActiveDirection.EW)
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
             
            if (currentActiveDirection == ActiveDirection.NS) // implies EW
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
            Debug.Log("Light is facing North");
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

    public void StartTrafficCycle(Stoplight stoplight)
    {
        StartCoroutine(CycleLights(stoplight));
    }

    private IEnumerator CycleLights(Stoplight stoplight)
    {
            ActivateGo(stoplight);
            yield return new WaitForSeconds(greenDuration);
            ActivateYield(stoplight);
            yield return new WaitForSeconds(yellowDuration);
            ActivateStop(stoplight);
            //yield return new WaitForSeconds(redDuration);
        
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


