using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiWindow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Activate all additional displays (if available)
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


}
