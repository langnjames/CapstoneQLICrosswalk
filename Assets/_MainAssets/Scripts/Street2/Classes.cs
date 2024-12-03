using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Classes
{
    
    
}

public class CarClass
{

    public string DetermineOrientation(GameObject car)
    {
        string direction = "NA";

        // Get the Y rotation in degrees
        float yRotation = car.transform.eulerAngles.y;

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
            //Debug.Log("Light is at an intermediate angle");
        }

        return direction;
    }

    private bool IsAngleWithinThreshold(float angle, float targetAngle, float threshold)
    {
        return Mathf.Abs(Mathf.DeltaAngle(angle, targetAngle)) <= threshold;
    }

    public void GetOrientation(Transform carTransform)
    {
        if (carTransform != null)
        {
            var tempTrans = carTransform;
            //Debug.Log(carTransform.forward);
            if (tempTrans.forward == Vector3.forward)
            {
                
            }
        }
    }
    public static Vector3 GetCarPosition(Car car)
    {
        return  new Vector3(0,0,0);
    }

    public static void StartCarMovement(Car car)
    {

    }

    public static void StopCarMovement(Car car)
    {

    }
}

public class Patient
{

}

public class Therapist
{

}

public class Lights
{

}

public class Stoplight
{
    public string direction;
    public GameObject stoplight;
    public GameObject greenLight;
    public GameObject yellowLight;
    public GameObject redLight;

    // Materials (shared among all stoplights)
    public static Material activeGreenMat;
    public static Material dimGreenMat;
    public static Material activeRedMat;
    public static Material dimRedMat;
    public static Material activeYellowMat;
    public static Material dimYellowMat;

    // Enum to track the current state
    public enum LightState
    {
        Red,
        Yellow,
        Green
    }

    public LightState currentState;

    public void SetStopLight(GameObject stoplightObj, string dir)
    {
        if (stoplightObj != null)
        {
            stoplight = stoplightObj;
            greenLight = stoplight.transform.Find("lightGreen").gameObject;
            yellowLight = stoplight.transform.Find("lightYellow").gameObject;
            redLight = stoplight.transform.Find("lightRed").gameObject;
        }
        if (dir != null)
        {
            direction = dir;
        }

        // Initialize the stoplight to red
        ActivateStop();
    }

    public static void SetMaterials(Material activeGreen, Material dimGreen, Material activeRed, Material dimRed, Material activeYellow, Material dimYellow)
    {
        activeGreenMat = activeGreen;
        dimGreenMat = dimGreen;
        activeRedMat = activeRed;
        dimRedMat = dimRed;
        activeYellowMat = activeYellow;
        dimYellowMat = dimYellow;
    }

    public string GetDirection()
    {
        return this.direction;
    }

    public void SetDirection(string direction)
    {
        this.direction = direction;
    }

    public void ActivateGo()
    {
        currentState = LightState.Green;
        greenLight.GetComponent<MeshRenderer>().material = activeGreenMat;
        yellowLight.GetComponent<MeshRenderer>().material = dimYellowMat;
        redLight.GetComponent<MeshRenderer>().material = dimRedMat;
    }

    public void ActivateYield()
    {
        currentState = LightState.Yellow;
        greenLight.GetComponent<MeshRenderer>().material = dimGreenMat;
        yellowLight.GetComponent<MeshRenderer>().material = activeYellowMat;
        redLight.GetComponent<MeshRenderer>().material = dimRedMat;
    }

    public void ActivateStop()
    {
        currentState = LightState.Red;
        greenLight.GetComponent<MeshRenderer>().material = dimGreenMat;
        yellowLight.GetComponent<MeshRenderer>().material = dimYellowMat;
        redLight.GetComponent<MeshRenderer>().material = activeRedMat;
    }

    public bool IsGreen()
    {
        return currentState == LightState.Green;
    }

    public bool IsRed()
    {
        return currentState == LightState.Red;
    }

    public string GetState()
    {
        if (direction == "N" || direction == "S")
        {
            if (IsRed())
            {
                return "NS";
            }
            else
            {
                return "EW";
            }
        }
        if (direction == "E" || direction == "W")
        {
            if (IsRed())
            {
                return "EW";
            }
            else
            {
                return "NS";
            }

        }

        return "";
    }
}

