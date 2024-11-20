using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trafficManagerSimple : MonoBehaviour
{
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


    // if a timer was made yet
    private bool timerCreated = false;
    // the current timer value
    private float theTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        // Keep status within bounds
        if (status < 0)
            status = 0;
        if (status > 2)
            status = 2;
    
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
                        status = 0;
                    }
                    // change to next
                }
            }

        }      
    }

    // Tell the light you want to walk
    public void TriggerWalk()
    {
        walkTriggered = true;
        timerCreated = false;
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
