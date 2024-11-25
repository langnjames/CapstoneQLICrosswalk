using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;



// The TouchButton class inherits from XRBaseInteractable to handle VR/AR interactions
public class TouchButton : XRBaseInteractable
{
    

    public int buttonValue;

    // Awake is called when the script instance is being loaded
    protected override void Awake()
    {
        // Call the base class's Awake method
        base.Awake();
        
    }

    // This method is called when the hover interaction starts
    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        // Log a message to the console for debugging
        // Debug.Log("Hover Entered");
        // Call the base class's OnHoverEntered method
        base.OnHoverEntered(args);
        Debug.Log(gameObject.name);
        if (gameObject.name == "End Button")
        {
            Debug.Log("Call end game");
            GameManager.Instance.EndGame();
        }
        if (gameObject.name == "crosswalk Button")
        {
            trafficManagerSimple.Instance.TriggerWalk();
        }
    }

    // This method is called when the hover interaction ends
    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        // Log a message to the console for debugging
        // Debug.Log("Hover Exited");
        // Call the base class's OnHoverExited method
        base.OnHoverExited(args);
        // If the Renderer component and the original material are not null, change the object's material back to the original material
        Debug.Log("Got here");
    }
}
