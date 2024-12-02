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

    #endregion

}
