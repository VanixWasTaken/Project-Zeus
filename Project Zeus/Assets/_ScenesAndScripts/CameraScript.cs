using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{

    float speed = 12.0f;
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



   


    // Reads the Input (Found under Input/Camera/Move) and moves the camera on the horizontal axes based on that
    private void HandleMovement()
    {
        Vector2 inputVector = inputActions.Camera.Move.ReadValue<Vector2>();

        direction = new Vector3(inputVector.x, 0f, inputVector.y).normalized;

        direction = Quaternion.AngleAxis(-45, Vector3.up) * direction;

        if (direction.magnitude >= 0.1f)
        {
            currentVelocity = direction * speed;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

}
