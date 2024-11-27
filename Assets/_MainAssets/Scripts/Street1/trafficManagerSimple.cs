using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class trafficManagerSimple : MonoBehaviour
{
    public static trafficManagerSimple Instance { get; private set; }

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
    private float walkTimer = 0f;
    private float timeToWalk = 0f;

    AudioSource audioSource;

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
        //SetWalkTimer();
    }

   

    // Update is called once per frame
    void Update()
    {
        // Keep status within bounds
        if (status < 0)
            status = 0;
        if (status > 2)
            status = 2;
    
        if (status == 0)
        {
            //Countdown Timer
            walkTimer -= Time.deltaTime;
            if (walkTimer < 0f)
            {
                if (walkInProgress) // If the person started the timer manually this is turned to true. 
                {
                    GameManager.Instance.ResetScene(); // This can be anything but I decided to end the game when person didn't complete it
                }
                Invoke("ResetLights", 0f);
            }
            
        }

        Debug.Log("Weird: " + MenuSettings.Instance.walkTimer);

        // Prepare to walk
        if (walkTriggered)
        {
            // Make a new timer
            if(!timerCreated)
            {
                //set the new timer value
                theTimer = intoSlow;
                timerCreated = true;
            }
            else
            {
                // count down
                if(theTimer > 0f)
                {
                    // subtract the time elapsed
                    theTimer -= Time.deltaTime;
                    //Debug.Log("The timer: "+ theTimer);
                }
                // timer complete
                else
                {
                    if(status == 2) // Change to slow state
                    {
                        status = 1;
                        theTimer = intoStop; //start next timer
                    }
                    else if(status == 1) // Change to stop state
                    {
                        
                        walkTimer = MenuSettings.Instance.walkTimer;
                        walkInProgress = true;
                        status = 0;
                        
                    }
                    // change to next
                }
            }

        }      
    }

    public void ResetLights()
    {
        Debug.Log("Invoked Reset lights");
        status = 2;
        walkTriggered = false;
        timerCreated= false;
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
    public string getStatus()
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
