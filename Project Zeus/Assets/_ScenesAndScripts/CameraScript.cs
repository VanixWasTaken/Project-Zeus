using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    #region References

    private InputActions inputActions;

    #endregion

    #region Variables

    private float speed = 12.0f;
    private Vector3 currentVelocity;
    private Vector3 direction;

    private Vector2 mousePosition;

    #endregion



    #region Unity Built-In

    private void Awake()
    {
        inputActions = new InputActions();
    }

    private void Start()
    {
        inputActions.Camera.Enable(); // enables the input for the camera
    }
   
    private void Update()
    {
        HandleMovement(); // updates the camera position based on player input

        CameraMouseMovement();
    }

    #endregion


    #region Custom Functions()

    private void HandleMovement()
    {
        /// <summary>
        /// Reads the Input (Found under Input/Camera/Move) and moves the camera on the horizontal axis based on that
        /// </summary>
        Vector2 inputVector = inputActions.Camera.Move.ReadValue<Vector2>();

        direction = new Vector3(inputVector.x, 0f, inputVector.y).normalized;

        direction = Quaternion.AngleAxis(-45, Vector3.up) * direction;

        if (direction.magnitude >= 0.1f)
        {
            currentVelocity = direction * speed;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void CameraMouseMovement()
    {
        mousePosition = Mouse.current.position.ReadValue(); // Get the mouse position using the new Input System

        Vector3 movement = Vector3.zero;
        

        // Get the screen dimensions
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Check edges and apply movement
        if (mousePosition.x >= 0 && mousePosition.x < 10f) // Left edge
        {
            movement.x -= 1;
        }
        else if (mousePosition.x <= screenWidth && mousePosition.x > screenWidth - 10f) // Right edge
        {
            movement.x += 1;
        }

        if (mousePosition.y >= 0 && mousePosition.y < 10f) // Bottom edge
        {
            movement.z -= 1; // Assuming camera forward/backward movement is along the Z axis
        }
        else if (mousePosition.y <= screenHeight && mousePosition.y > screenHeight - 10f) // Top edge
        {
            movement.z += 1;
        }


        // Apply the same -45 degree rotation to the mouse movement
        movement = Quaternion.AngleAxis(-45, Vector3.up) * movement;

        // Normalize and apply speed
        movement = movement.normalized * speed * Time.deltaTime;

        // Apply movement
        transform.Translate(movement, Space.World);
    }

    private void TurnCamera()
    {
        if (inputActions.Keyboard.TurnCameraRight)
    }

    #endregion

}
