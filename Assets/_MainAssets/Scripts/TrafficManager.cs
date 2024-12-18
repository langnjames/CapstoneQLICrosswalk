using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Rendering;

public class TrafficManager : MonoBehaviour
{
    public static TrafficManager Instance { get; private set; }

    // 0 = STOP
    // 1 = SLOW DOWN
    // 2 = GO/DRIVE
    private int status = 2;
    // How long it takes to change to slow
    private float intoSlow = 3f;
    // How long it takes to change to stop
    private float intoStop = 3f;
    // If the walk sign was triggered
    public bool walkTriggered = false;

    public AudioClip buttonClickAudio;

    public bool walkInProgress = false;

    // if a timer was made yet
    private bool timerCreated = false;
    // the current timer value
    private float theTimer = 0f;
    public float walkTimer = 0f;
    private float timeToWalk = 0f;
    private float defaultWalkTimer = 20f;

    AudioSource audioSource;

    private float greenDuration;
    private float yellowDuration = 3f;
    private float redDuration;
    private float lightSwapDuration;

    public enum ActiveDirection
    {
        NS,
        EW,
        None
    }

    public ActiveDirection activeDirection = ActiveDirection.EW;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        
    }

    private void Start()
    {
        CalculateLightCycles();
        theTimer = walkTimer;
    }

    public float GetTimer()
    {
        return theTimer;
    }

    public ActiveDirection GetDirection()
    {
        return activeDirection;
    }

    // Update is called once per frame
    void Update()
    {
       



        // THIS IS THE MAIN INTERSECTION LOOP WITHOUT PRESSING TRAFFIC BUTTON     
        
        theTimer -= Time.deltaTime; // decrease timer through update
        
        if (theTimer >= walkTimer - greenDuration) // if the timer ran for green light duration
        {
            status = 2; // Set status to go
        }
        else if (theTimer <= walkTimer - lightSwapDuration) // if the timer ran over the duration of light cycle
        {
            status = 0; // Set status to stop
            SwapDirection(); // Change the direction
            theTimer = walkTimer; // Restart the timer
        }
        else
        {
            status = 1; // Set the status to yellow light / slow down
        }
        //Debug.Log("TIMER: " + theTimer);


        //if (status == 0)
        //{
        //    //Countdown Timer
        //    walkTimer -= Time.deltaTime;
        //    if (walkTimer < 0f)
        //    {
        //        if (walkInProgress) // If the person started the timer manually this is turned to true. 
        //        {
        //            GameManager.Instance.ResetScene("You failed to cross the crosswalk in time"); // This can be anything but I decided to end the game when person didn't complete it
        //        }
        //        Invoke("ResetLights", 0f);
        //    }

        //}

        // Prepare to walk
        if (walkTriggered)
        {
            // Make a new timer
            if (!timerCreated)
            {

                //set the new timer value
                theTimer = intoSlow;
                timerCreated = true;
            }
            else
            {
                //if (theTimer > 0f)
                //{
                //    // subtract the time elapsed
                //    //theTimer -= Time.deltaTime;
                //    //Debug.Log("The timer: "+ theTimer);
                //}
                //// timer complete
                //else
                //{
                //    if (status == 2) // Change to slow state
                //    {
                //        status = 1;
                //        theTimer = intoStop; //start next timer
                //    }
                //    else if (status == 1) // Change to stop state
                //    {
                //        if (MenuSettings.Instance != null)
                //        {
                //            walkTimer = MenuSettings.Instance.walkTimer;
                //        }
                //        else
                //        {
                //            walkTimer = defaultWalkTimer;
                //        }

                //        walkInProgress = true;
                //        status = 0;
                //    }
                //    // change to next
                //}
            }

        }
    }

    public void ResetLights()
    {
        Debug.Log("Invoked Reset lights");
        status = 2;
        walkTriggered = false;
        timerCreated = false;
        
    }

    public void CalculateLightCycles()
    {
        //Init


        ////Confirm wether Menu scene was initialized or not
        //if (MenuSettings.Instance != null)
        //{
        //    walkTimer = MenuSettings.Instance.walkTimer;
        //}
        //else
        //{
        //    walkTimer = defaultWalkTimer;
        //}
        walkTimer = MenuSettings.Instance.walkTimer;

        Debug.Log("walkTimer: " + walkTimer);

        greenDuration = walkTimer - yellowDuration;
        redDuration = greenDuration;
        lightSwapDuration = greenDuration + yellowDuration;

        Debug.Log("GREEN" + greenDuration + "\nRED: " + redDuration + "SWAP: "+lightSwapDuration);


    }

    public void SwapDirection()
    {
        if (activeDirection == ActiveDirection.EW)
        {
            activeDirection = ActiveDirection.NS;
        }
        else
        {
            activeDirection = ActiveDirection.EW;
        }
    }


    // Tell the light you want to walk
    public void TriggerWalk()
    {
        walkTriggered = true;
        timerCreated = false;

        audioSource.PlayOneShot(buttonClickAudio);
        Debug.Log("Triggered Sign");
    }

    // Get the status of the light
    public string GetStatus()
    {
        // say stop
        if (status == 0)
            return "stop";
        // say slow down
        else if (status == 1)
            return "slow";
        // say go
        else
            return "go";
    }
}
