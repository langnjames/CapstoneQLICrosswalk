using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    GameObject car;
    public float movementSpeed = 7f;
    public CarClass carObject = new CarClass();
    public Stoplight stoplight;  // Reference to the StoplightScript to check the light state

    private bool pastIntersection = false;


    // Start is called before the first frame update
    void Start()
    {
        car = this.gameObject;
        Time.fixedDeltaTime = 1 / 60f;
        stoplight = Component.FindAnyObjectByType<StoplightScript>().stoplightDict.GetValueOrDefault(0) ;


        
    }

    private void OnEnable()
    {
        CollisionEventHandler.OnCollisionEnterEvent += SetCarFlag;
    }

    private void OnDisable()
    {
        CollisionEventHandler.OnCollisionEnterEvent -= SetCarFlag;
    }

    void SetCarFlag()
    {
        pastIntersection = true;
    }

    // FixedUpdate is called on a fixed interval (recommended for physics calculations)
    void FixedUpdate()
    {
        if (CanMoveForward())
        {
            MoveForward();
        }
        else
        {
            Brake();
        }
    }

    void MoveForward()
    {
        movementSpeed = 7f;
        car.transform.position -= car.transform.up * movementSpeed * .01f;
    }

    void Brake()
    {
        // Optionally reduce speed smoothly or stop completely
        movementSpeed = 0f;
    }

    bool CanMoveForward()
    {
        // Determine the car's direction using the `DetermineOrientation` method from Car class
        string carDirection = carObject.DetermineOrientation(car);

        // Find the corresponding stoplight from the dictionary in StoplightScript based on the car's direction

        //if (pastIntersection)
        //{
        //    return true;
        //}

        if (stoplight == null)
        {
            Debug.Log("NULL");
        }
        if (carDirection == "E" || carDirection == "W") // if south stoplight is green
        {
            if (!Stoplight.IsRed(stoplight))
            {
                return true;
            }
        }
        if (carDirection == "N" || carDirection == "S")
        {
            Debug.Log("Here");
            if(Stoplight.IsRed(stoplight))
            {
                return true;
            }
        }
        Debug.Log("stoplight green:" + Stoplight.IsGreen(stoplight)  + "Stoplight: " + stoplight.direction+ ", cardirection: " + carDirection);
           
        

        // Default to false if no valid light is found
        return false;
    }
}
