using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEventHandler : MonoBehaviour
{
    public delegate void ChildCollisionHandler();
    public static event ChildCollisionHandler OnCollisionEnterEvent;
    public static event ChildCollisionHandler OnCollisionExitEvent;

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name.Contains("car"))
        {
            //Debug.Log("Called this bc: " + other.gameObject.name);
            OnCollisionEnterEvent?.Invoke();
        }
        
    }

    
}
