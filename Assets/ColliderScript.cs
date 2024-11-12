using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public delegate void ChildCollisionHandler();
    public static event ChildCollisionHandler OnCollisionEnterEvent;
    public static event ChildCollisionHandler OnCollisionExitEvent;

    void OnTriggerEnter(Collider other)
    {
        OnCollisionEnterEvent?.Invoke();
    }

    void OnTriggerExit(Collider other)
    {
        OnCollisionExitEvent?.Invoke();
    }
}