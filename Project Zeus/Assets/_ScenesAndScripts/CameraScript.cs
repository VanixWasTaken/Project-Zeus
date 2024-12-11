using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    #region References

    private InputActions inputActions;
    private Transform cameraFollowTransform;
    public CinemachineCamera cinCam;

    #endregion

    #region Variables

    private Vector3 currentVelocity;
    private Vector3 direction;
    private Vector2 mousePosition;

    [Header("Game Design Variables")]
    public float camSpeed = 12f;
    public float zoomRate = 3f;
    public float rotationSpeed = 0.5f;
    public float maxFOV = 60;
    public float minFOV = 30;
    #endregion



    #region Unity Built-In

    private void Awake()
    {
        inputActions = new InputActions();

        cameraFollowTransform = GetComponent<Transform>();
    }

    private void Start()
    {
        inputActions.Camera.Enable(); // enables the input for the camera
    }
   
    private void Update()
    {
        HandleMovement(); // updates the camera position based on player input

        CameraMouseMovement();

        TurnCamera();

        Zoom();

        ResetCam();
    }

    #endregion


    #region Custom Functions()

    private void HandleMovement()
    {
        /// <summary>
        /// Reads the Input (Found under Input/Camera/Move) and moves the camera relative to its local rotation
        /// </summary>
        Vector2 inputVector = inputActions.Camera.Move.ReadValue<Vector2>();

        direction = new Vector3(inputVector.x, 0f, inputVector.y).normalized;

        direction = cameraFollowTransform.rotation * direction;

        if (direction.magnitude >= 0.1f)
        {
            currentVelocity = direction * camSpeed;
            transform.position += currentVelocity * Time.deltaTime;
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
        movement = cameraFollowTransform.rotation * movement;

        // Normalize and apply speed
        movement = movement.normalized * camSpeed * Time.deltaTime;

        // Apply movement
        transform.Translate(movement, Space.World);
    }

    private void TurnCamera()
    {
        if (inputActions.Camera.TurnCameraRight.IsPressed()) // Turns the camera to the right
        {
            cameraFollowTransform.Rotate(0, -rotationSpeed * Time.deltaTime, 0, Space.World);
        }

        else if (inputActions.Camera.TurnCameraLeft.IsPressed()) // Turns the camera to the left
        {
            cameraFollowTransform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
        }
    }

    private void Zoom()
    {
        if (inputActions.Camera.ZoomIn.WasPerformedThisFrame() && cinCam.Lens.FieldOfView > minFOV)
        {
            cinCam.Lens.FieldOfView -= zoomRate;
        }

        else if (inputActions.Camera.ZoomOut.WasPerformedThisFrame() && cinCam.Lens.FieldOfView < maxFOV)
        {
            cinCam.Lens.FieldOfView += zoomRate;
        }
    }

    private void ResetCam()
    {
        if (inputActions.Camera.ResetCamera.WasPerformedThisFrame())
        {
            transform.rotation = Quaternion.Euler(0, -45, 0);
            cinCam.Lens.FieldOfView = maxFOV;
        }
    }

    #endregion

}
