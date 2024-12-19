using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carSpawner : MonoBehaviour
{

    [Header("Enable the spawner")]
    public bool isEnabled = false; // if this is allowed to run

    [Header("Spawning time")]
    public float spawnInterval = 3f; // how long to wait before spawning again
    public float spawnInterval_Offset = 2f; // randomized delay time added on

    [Header("Spawn this object")]
    public GameObject theCar; // the car object to spawn

    // if a timer was made yet
    private bool timerCreated = false;
    // the current timer value
    private float theTimer = 0f;

    // The object to look at for instructions
    private TrafficManager TrafficManager;

    //
    private bool spawnAllowed = true;
    private float waitTimer = 0f;

    void Start()
    {
        // Get the traffic manager object's component
        TrafficManager = TrafficManager.Instance;
        setTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnAllowed)
        {    
            waitTimer -= Time.deltaTime;
            if (waitTimer < 0f)
            {
                SwapEnabled();
            }

            if (waitTimer > 100f && TrafficManager.Instance.GetStatus() == "slow")
            {
                spawnAllowed = false;
            }


                // Make a timer if there isn't one
            if (!timerCreated)
            {
                // Generate number
                theTimer = spawnInterval + Random.Range(0f, spawnInterval_Offset);
                // Signal that the timer was made
                timerCreated = true;
            }
            else if (isEnabled && timerCreated)
            {
            // count down
                if (theTimer > 0f)
                {
                    // subtract the time elapsed
                    theTimer -= Time.deltaTime;
                }
                // timer complete
                else
                {
                    timerEnded();
                    timerCreated = false;
                }
            }
        }
    }

    //Sets the interval timing based on the menu settings
    void setTimer()
    {
        int spawnRate;
        spawnRate = MenuSettings.Instance.trafficLevel;
        //Debug.Log("spawnRate:" + spawnRate);


        switch (spawnRate) 
        {
            case 0:
                disableSpawner(); spawnAllowed = false; break;
            case 1:
                spawnInterval = 7f; spawnAllowed = true; break; // Larger time intervals for spawning
            case 2:
                spawnInterval = 3f; spawnAllowed = true; break; // Normal time intervals for spawning
            case 3:
                spawnInterval = .75f; spawnAllowed = true; break; // Short time intervals for spawning
        }
    }

    public void SwapEnabled()
    {
        isEnabled = !isEnabled;
        waitTimer = TrafficManager.Instance.walkTimer;
    }

    void timerEnded()
    {
        // Spawn the object and set its position and rotation to THIS object
        GameObject newCar = Instantiate(theCar, transform.position, transform.rotation);
    }

    // Simply shuts off the spawner
    public void disableSpawner()
    {
        isEnabled = false;
    }

}
