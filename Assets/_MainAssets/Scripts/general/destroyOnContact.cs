using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnContact : MonoBehaviour
{
    [Header("Object gets destroyed when touching this tag")]
    // The name of the tag to look for
    public string TagName = "ObjectDestroyer";

    // Upon collision
    void OnTriggerEnter(Collider collider) 
	{
        // Check the collided object's tag
		if(collider.gameObject.tag == TagName)
		{
			// destroy this object
			Destroy(this.gameObject);
		}
	}

}
