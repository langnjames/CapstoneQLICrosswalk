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
        // Move the rig so that the camera ends up at desiredCamPosition
        Vector3 offset = desiredCamPosition - mainCamera.transform.position;
        xrRigTransform.position += offset;

        // Reset rig rotation if needed
        xrRigTransform.rotation = Quaternion.Euler(desiredRigEulerAngles);
    }
}
