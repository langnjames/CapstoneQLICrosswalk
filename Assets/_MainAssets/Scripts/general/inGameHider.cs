using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Makes the object INVISIBLE in game mode
public class inGameHider : MonoBehaviour
{
    Renderer test;

    // Start is called before the first frame update
    void Start()
    {
        // Get the MeshRender component
        test = GetComponent<MeshRenderer>();

        // Disable the mesh render
        test.enabled = false;
    }
}
