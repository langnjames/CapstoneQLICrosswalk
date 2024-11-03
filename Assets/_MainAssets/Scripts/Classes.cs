using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Classes
{
    
    
}

public class Car
{
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
    public  GameObject greenLight;
    public  GameObject yellowLight;
    public  GameObject redLight;

    // Materials (These can be static if shared among all stoplights)
    public static Material activeGreenMat;
    public static Material dimGreenMat;
    public static Material activeRedMat;
    public static Material dimRedMat;

    public void SetStopLight(GameObject stoplight, string direction) 
    {
        if(stoplight != null) 
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

    public static void SetMaterials(Material activeGreen, Material dimGreen, Material activeRed, Material dimRed)
    {
        activeGreenMat = activeGreen;
        dimGreenMat = dimGreen;
        activeRedMat = activeRed;
        dimRedMat = dimRed;
    }

    public void ActivateGo()
    {
        this.greenLight.GetComponent<MeshRenderer>().material = activeGreenMat;
        this.redLight.GetComponent<MeshRenderer>().material = dimRedMat;
        
        //Set Walkbox to walk sign
    }

    public void ActivateStop()
    {
        this.greenLight.GetComponent<MeshRenderer>().material = dimGreenMat;
        this.redLight.GetComponent<MeshRenderer>().material = activeRedMat;

        // Set Walkbox to Stop hand

    }

    public static bool IsGreen(Stoplight stoplight)
    {
        Material currentMaterial = stoplight.greenLight.GetComponent<MeshRenderer>().material;
        string materialName = currentMaterial.name.Replace(" (Instance)", "");
        return materialName == activeGreenMat.name;
    }


    public static string GetState(Stoplight stoplight)
    {
        if (stoplight.direction == "N" ||  stoplight.direction == "S")
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

    //public static bool IsNSActive(Stoplight stoplight)
    //{
    //    bool active = false;
    //    bool lightActive = stoplight.greenLight.GetComponent<MeshRenderer>().material == activeGreenMat;
    //    bool lightInactive = stoplight.greenLight.GetComponent<MeshRenderer>().material == dimGreenMat;

    //    if (lightActive)
    //    {
    //        if (stoplight.direction == "N" || stoplight.direction == "S")
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            active =  false;
    //        }
    //    }
    //    else if (lightInactive) 
    //    {
    //        if (stoplight.direction == "E" || stoplight.direction == "W")
    //        {
    //            active =  true;
    //        }
    //        else
    //        {
    //            active = false;
    //        }
    //    }
    //    return active;
    //}
}
