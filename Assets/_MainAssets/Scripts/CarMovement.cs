using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{

    GameObject car;
    public float movementSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        car = this.gameObject;
        Time.fixedDeltaTime = 1 / 60f;
        Debug.Log(car.gameObject.transform.forward);
    }

    // FixedUpdate is called on a fixed interval (recommended for physics calculations)
    void FixedUpdate()
    {
        car.transform.position -= car.transform.up * movementSpeed * Time.fixedDeltaTime;
    }

}
