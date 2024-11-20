using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carSimple : MonoBehaviour
{
    
    // The speeds to reference
    [Header("Car Speeds")]
    public float speedRegular = 20f;
    public float speedSlow = 8f;
    public float speedStop = 0f;

    // The object's current speed
    private float speed = 0f;


    // The object to look at for instructions
    private trafficManagerSimple TrafficManager;

    void Start()
    {
        // Get the traffic manager object's component
        TrafficManager = GameObject.FindWithTag("TrafficManager").GetComponent<trafficManagerSimple>();

        speed = speedRegular;
    }

    // Update is called once per frame
    void Update()
    {
        // move forward (along Z)
        transform.position += transform.forward * speed * .01f;   
    }

    // Upon collision
    void OnTriggerEnter(Collider collider) 
	{
        // JUST GO if you touch a go spot
        if (collider.gameObject.tag == "GoSpot")
            speed = speedRegular;

        // STOP if touching a car
		else if(collider.gameObject.tag == "CAR")
			speed = speedStop;
        
        // If touching the STOP spot during a stop sign
		else if(collider.gameObject.tag == "StopSpot" && TrafficManager.getStatus() == "stop")
			speed = speedStop;
        
        // SLOW if touching the SLOW spot while the go sign is disabled
		else if(collider.gameObject.tag == "SlowSpot" && TrafficManager.getStatus() != "go")
			speed = speedSlow;
	}
}
