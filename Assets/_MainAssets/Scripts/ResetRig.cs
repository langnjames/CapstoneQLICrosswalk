using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRig : MonoBehaviour
{
    public Transform xrRigTransform;
    public Camera mainCamera;
    public Vector3 desiredCamPosition;
    public Vector3 desiredRigEulerAngles;

    public void ResetRigPosition()
    {
        // Get the current Y rotation of the camera and the desired Y rotation
        float currentY = mainCamera.transform.eulerAngles.y;
        float desiredY = desiredRigEulerAngles.y;

        // Compute how much we need to rotate around Y to align with the desired Y angle
        float yOffset = desiredY - currentY;

        // Rotate the rig only around the Y-axis
        xrRigTransform.Rotate(0f, yOffset, 0f, Space.World);

        // Now adjust the rig position to place the camera at the desiredCamPosition
        Vector3 offset = desiredCamPosition - mainCamera.transform.position;
        xrRigTransform.position += offset;
    }

    public void Start()
    {
        ResetRigPosition();
    }
}
