using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{

    float speed = 6.0f;
    Vector3 currentVelocity;
    Vector3 direction;

    InputActions inputActions;

    GameObject test;




    void Awake()
    {
        inputActions = new InputActions();
    }

    void Start()
    {
        inputActions.Camera.Enable();


        
    }
   
    void Update()
    {
        HandleMovement();


    }



   



    private void HandleMovement()
    {
        Vector2 inputVector = inputActions.Camera.Move.ReadValue<Vector2>();
        Debug.Log("Input Vector: " + inputVector);  // Log the input values to check

        direction = new Vector3(inputVector.x, 0f, inputVector.y).normalized;

        direction = Quaternion.AngleAxis(-45, Vector3.up) * direction;

        if (direction.magnitude >= 0.1f)
        {
            Debug.Log("Moving camera");
            currentVelocity = direction * speed;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

}
