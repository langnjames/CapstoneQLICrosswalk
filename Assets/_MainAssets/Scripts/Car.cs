using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Car : MonoBehaviour
{
    private StoplightScript stoplights;
    Stoplight stoplight;
    private float carRandSpeed;
    private float carCurrentSpeed = 10;
    private string carDirection;
    int counter = 0;


    // Start is called before the first frame update
    void Start()
    {
        Time.fixedDeltaTime = 1 / 60f; // 60 fps update loop
        carRandSpeed = Random.Range(5f, 10f);
        carCurrentSpeed = carRandSpeed;

        //Get a value of stoplight to make calls from
        stoplights = GameObject.Find("-- STOPLIGHTS --").GetComponent<StoplightScript>();
        stoplight = stoplights.stoplightDict[0];


        carDirection = DetermineOrientation(this.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        counter++;
        if (!StopForRed())
        {
            if (carCurrentSpeed < carRandSpeed)
            {
                SpeedUp();
            }
            Drive();
        }
        else
        {
            SlowDown();
        }
    }

    void Drive()
    {
        this.transform.position += .1f * carRandSpeed * this.transform.forward;
    }

    void SlowDown()
    {
        if (carCurrentSpeed > 0)
        {
            carCurrentSpeed -= .1f;
        }
        else
        {
            carCurrentSpeed = 0;
        }

        this.transform.position +=  .1f * carCurrentSpeed * this.transform.forward;
    }

    void SpeedUp()
    {
        if (carCurrentSpeed <= carRandSpeed)
        {
            carCurrentSpeed += .1f;
        }
        else
        {
            carCurrentSpeed = carRandSpeed;
        }

        this.transform.position += .1f * carCurrentSpeed * this.transform.forward;
    }

    bool StopForRed()
    {

        if (stoplight == null)
        {
            Debug.Log("NULL");
        }
        if (carDirection == "E" || carDirection == "W")
        {
            if (Stoplight.IsRed(stoplight)) // East LIGHT: if east red then need to stop
            {
                Debug.Log(this.gameObject.name + " needs to stop");
                return true;
            }
            else if (!Stoplight.IsRed(stoplight))
            {
                Debug.Log(this.gameObject.name + " is good to drive");
                return false;
            }
        }
        else //if (carDirection == "N" || carDirection == "S")
        {
            Debug.Log(stoplight.direction + ", NS car");
            if (Stoplight.IsRed(stoplight)) // East LIGHT: if east red then don't need to stop
            {
                Debug.Log(this.gameObject.name + " is good to drive");
                return false;
            }
            else if (!Stoplight.IsRed(stoplight))
            {
                Debug.Log(this.gameObject.name + " needs to stop");
                return true;
            }
        }

        return false;
    }



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
            Debug.Log("Object is facing North: " + car.name);
        }
        else if (IsAngleWithinThreshold(yRotation, 90f, threshold))
        {
            direction = "E";
            Debug.Log("Object is facing East: " + car.name);
        }
        else if (IsAngleWithinThreshold(yRotation, 180f, threshold))
        {
            direction = "S";
            Debug.Log("Object is facing South: " + car.name);
        }
        else if (IsAngleWithinThreshold(yRotation, 270f, threshold))
        {
            direction = "W";
            Debug.Log("Object is facing West: " + car.name);
        }
        else
        {
            Debug.Log("Object is at an intermediate angle");
        }

        return direction;
    }

    private bool IsAngleWithinThreshold(float angle, float targetAngle, float threshold)
    {
        return Mathf.Abs(Mathf.DeltaAngle(angle, targetAngle)) <= threshold;
    }
}
