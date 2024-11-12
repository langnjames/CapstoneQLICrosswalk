using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Classes
{
    
    
}

public class Car
{

    public void GetOrientation(Transform carTransform)
    {
        if (carTransform != null)
        {
            var tempTrans = carTransform;
            Debug.Log(carTransform.forward);
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

    // Materials (These can be static if shared among all stoplights)
    public static Material activeGreenMat;
    public static Material dimGreenMat;
    public static Material activeRedMat;
    public static Material dimRedMat;
    public static Material activeYellowMat;
    public static Material dimYellowMat;


    public void SetStopLight(GameObject stoplight, string direction)
    {
        if (stoplight != null)
        {
            this.stoplight = stoplight;
            greenLight = stoplight.transform.Find("lightGreen").gameObject;
            yellowLight = stoplight.transform.Find("lightYellow").gameObject;
            redLight = stoplight.transform.Find("lightRed").gameObject;
        }
        if (direction != null)
        {
            this.direction = direction;
        }
    }

    public string GetDirection()
    {
        return this.direction;
    }

    public void SetDirection(string direction)
    {
        this.direction = direction;
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

    public void ActivateGo()
    {
        this.greenLight.GetComponent<MeshRenderer>().material = activeGreenMat;
        this.redLight.GetComponent<MeshRenderer>().material = dimRedMat;
    }


    public void ActivateYield()
    {
        this.greenLight.GetComponent<MeshRenderer>().material = dimGreenMat;
        this.yellowLight.GetComponent<MeshRenderer>().material = activeYellowMat;
    }

    public void ActivateStop()
    {
        this.yellowLight.GetComponent<MeshRenderer>().material = dimYellowMat;
        this.redLight.GetComponent<MeshRenderer>().material = activeRedMat;
    }

    public static bool IsGreen(Stoplight stoplight)
    {
        Material currentMaterial = stoplight.greenLight.GetComponent<MeshRenderer>().material;
        string materialName = currentMaterial.name.Replace(" (Instance)", "");
        return materialName == activeGreenMat.name;
    }


    public static string GetState(Stoplight stoplight)
    {
        if (stoplight.direction == "N" || stoplight.direction == "S")
        {
            if (IsGreen(stoplight))
            {
                return "NS";
            }
            else
            {
                return "EW";
            }
        }
        else // direction is EW
        {
            if (IsGreen(stoplight))
            {
                return "EW";
            }
            else
            {
                return "NS";
            }

        }
    }
}
