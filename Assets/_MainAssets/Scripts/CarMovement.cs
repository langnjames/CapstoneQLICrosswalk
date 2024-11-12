using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{

    GameObject car;
    public float movementSpeed = 0.5f;
    public Car carObject = new Car();

    // Start is called before the first frame update
    void Start()
    {
        car = this.gameObject;
        Time.fixedDeltaTime = 1 / 60f;
        Debug.Log(car.gameObject.transform.forward);
        carObject.GetOrientation(car.transform);
    }

    // FixedUpdate is called on a fixed interval (recommended for physics calculations)
    void FixedUpdate()
    {
        MoveForward();
    }

    void MoveForward()
    {
        car.transform.position -= car.transform.up * movementSpeed * Time.fixedDeltaTime;
    }

    void Brake()
    {
        SlowDown();
    }

    void SlowDown()
    {
        car.transform.position = car.transform.forward;

        // If the light for the direction is red: 
        // Begin lerping the positiions of the vector3's from where the car is at to where the car should stop.
        // I need there to be a stop point.
    }

}
