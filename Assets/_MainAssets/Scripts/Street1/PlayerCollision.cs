using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "CrosswalkSpot")
        {
            trafficManagerSimple.Instance.walkInProgress = true;
            //Debug.Log("Entered Crosswalk");
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "CrosswalkSpot")
        {
            trafficManagerSimple.Instance.walkInProgress = false;
        }
    }

}
