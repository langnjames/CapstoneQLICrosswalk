using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform targetCamera; // Assign the game camera's Transform

    void Update()
    {
        if (targetCamera != null)
        {
            transform.position = targetCamera.position;
            transform.rotation = targetCamera.rotation;
        }
    }
}
