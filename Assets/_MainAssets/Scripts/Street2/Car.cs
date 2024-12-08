using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Car : MonoBehaviour
{
    private StoplightScript stoplights;
    Stoplight stoplight;
    private float carMaxSpeed = 10f;
    private float carMinSpeed = 0f;
    private float carSpeed = 10f;
    private string carDirection;
    int counter = 0;


    // Start is called before the first frame update
    void Start()
    {
        Time.fixedDeltaTime = 1 / 60f; // 60 fps update loop
        //carMaxSpeed = Random.Range(5f, 10f);
        carSpeed = carMaxSpeed;

        //Get a value of stoplight to make calls from
        stoplights = GameObject.Find("-- STOPLIGHTS --").GetComponent<StoplightScript>();
        stoplight = stoplights.stoplightDict[0];

        //Get a NS light
        
        //Get a EW light



        carDirection = DetermineOrientation(this.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Drive();
        GoOnGreen();
    }

    void Drive()
    {
        // Other methods will set the car speed in order to make this stop, speed up, slow down, etc...
        this.transform.position += .1f * carSpeed * this.transform.forward;
    }

    void GoOnGreen()
    {
        if (!StopForRed())
        {
            carSpeed = carMaxSpeed;
        }
    }

    // Lerp to a speed via Coroutine
    private void SlowDown()
    {
        float lerpDuration = 3f;
        StartCoroutine(LerpCar(carMaxSpeed, carMinSpeed, lerpDuration));
    }

    // Lerp to a speed via Coroutine
    private void SpeedUp()
    {
        float lerpDuration = 3f;
        StartCoroutine(LerpCar(carMinSpeed, carMaxSpeed, lerpDuration));
    }

    // Stop the car immeadiately
    void StopCar()
    {
        carSpeed = 0;
        StopAllCoroutines();
    }

    // Will be implemented later for the car to make a turn or continue to go straight
    private void CarDecision()
    {
        float randChoice = Random.Range(0f, 1f);

        if(randChoice <= .1f)
        {
            // Car decides to turn left
        }
        else if(randChoice > .1f && randChoice <= .2f)
        {
            // Car decides to turn right
        }
        else
        {
            // Car moves forward
        }
    }

    // Method for checking if the car needs to stop for a red light
    // TRUE = STOP | FALSE = GO
    private bool StopForRed()
    {
        string TrafficStatus = TrafficController.Instance.GetStatus();
        if (carDirection == "E" || carDirection == "W")
        {
            if (TrafficStatus == "EW Stop" || TrafficStatus == "Yield") // East LIGHT: if east red then need to stop
            {
                //Debug.Log(this.gameObject.name + " needs to stop");
                return true;
            }
            else if (TrafficStatus == "NS Stop")
            {
                //Debug.Log(this.gameObject.name + " is good to drive");
                return false;
            }
        }
        else //if (carDirection == "N" || carDirection == "S")
        {
            //Debug.Log(stoplight.direction + ", NS car");
            if (TrafficStatus == "NS Stop" || TrafficStatus == "Yield") // East LIGHT: if east red then don't need to stop
            {
                //Debug.Log(this.gameObject.name + " is good to drive");
                return true;
            }
            else if (TrafficStatus == "EW Stop")
            {
                //Debug.Log(this.gameObject.name + " needs to stop");
                return false;
            }
        }

        return true;
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
            //Debug.Log("Object is facing North: " + car.name);
        }
        else if (IsAngleWithinThreshold(yRotation, 90f, threshold))
        {
            direction = "E";
            //Debug.Log("Object is facing East: " + car.name);
        }
        else if (IsAngleWithinThreshold(yRotation, 180f, threshold))
        {
            direction = "S";
            //Debug.Log("Object is facing South: " + car.name);
        }
        else if (IsAngleWithinThreshold(yRotation, 270f, threshold))
        {
            direction = "W";
            //Debug.Log("Object is facing West: " + car.name);
        }
        else
        {
            //Debug.Log("Object is at an intermediate angle");
        }

        return direction;
    }

    private bool IsAngleWithinThreshold(float angle, float targetAngle, float threshold)
    {
        return Mathf.Abs(Mathf.DeltaAngle(angle, targetAngle)) <= threshold;
    }

    IEnumerator LerpCar(float min, float max, float duration)
    {
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            carSpeed = Mathf.Lerp(min, max, timeElapsed / duration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        carSpeed = max;
    }

    private void OnTriggerEnter(Collider collider)
    {
        // JUST GO if you touch a go spot
        if (collider.gameObject.tag == "GoSpot")
        {

            carSpeed = carMaxSpeed;
        }

        // STOP if touching a car
        else if (collider.gameObject.tag == "CAR")
        {

            StopCar();
        }

        else if (collider.gameObject.tag == "SlowSpot")
        {
            if (StopForRed())
            {
                SlowDown();
            }
        }

        // If touching the STOP spot during a stop sign
        else if (collider.gameObject.tag == "StopSpot")
        {
            if (StopForRed())
            {

                StopCar();
            }
        }

        // SLOW if touching the SLOW spot while the go sign is disabled




        else if (collider.gameObject.tag == "Player")
        {
            // Stops the player from getting hit
            // Currently the cars stop extremely fast
            StopCar();
            GameManager.Instance.ResetScene("You almost got hit by a car");
        }
    }
}
