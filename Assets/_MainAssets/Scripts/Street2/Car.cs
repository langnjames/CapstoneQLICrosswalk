using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Car : MonoBehaviour
{
    private float carMaxSpeed = 10f;
    private float carMinSpeed = 0f;
    public float carSpeed = 10f;
    public bool carActive = true;

    [Header("Car Direction")]
    public Direction carDir;

    public enum Direction
    {
        EW,
        NS
    }

    private TrafficManager trafficManager;
    private string carDirection;

    // Start is called before the first frame update
    void Start()
    {
        Time.fixedDeltaTime = 1 / 60f; // 60 fps update loop
        //carMaxSpeed = Random.Range(5f, 10f);
        carSpeed = carMaxSpeed;

        ////Get a NS light

        ////Get a EW light

        trafficManager = TrafficManager.Instance;
        carDirection = carDir.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (carActive)
        {
            Drive();
            GoOnGreen();
        }
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
        string TrafficStatus = trafficManager.GetStatus();
        string activeDirection = trafficManager.activeDirection.ToString();
        if (carDirection == activeDirection)
        {
            if (TrafficStatus == "go") 
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }
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
        string activeDirection = trafficManager.activeDirection.ToString();
        // JUST GO if you touch a go spot
        if (collider.gameObject.tag == "GoSpot")
        {
            StopAllCoroutines();
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
            //Debug.Log("CAR: " + carDirection + "\tACTIVE: " + activeDirection + "\t STOP? : " + StopForRed().ToString());

            if (StopForRed())
            {

                StopCar();
            }
        }

        // SLOW if touching the SLOW spot while the go sign is disabled




        if (collider.gameObject.tag == "Player")
        {
            
            // Stops the player from getting hit
            // Currently the cars stop extremely fast
            carSpeed = 0;
            TrafficManager.Instance.StopCars();
            GameManager.Instance.ResetScene("You almost got hit by a car");
        }
    }
}
