using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CommandCenterStateManager : MonoBehaviour
{
    // All available CommandCenterStates
    #region CommandCenterStates
    CommandCenterBaseState currentState;
    public CommandCenterIdleState idleState = new CommandCenterIdleState();
    public CommandCenterClickedState clickedState = new CommandCenterClickedState();
    #endregion


    // All references
    #region References
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject commandCenterObject;
    public bool hoversAbove;
    public GameObject buildingButton;
    public GameObject playerButton;
    [SerializeField] UnitStateManager unit;
    InputActions inputActions;

    public TextMeshProUGUI extracttionUnitInfo;
    #endregion


    public int collectedCompleteEnergy;

    void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);

        inputActions = new InputActions();
        inputActions.Mouse.Enable();
    }

    void Update()
    {
        currentState.UpdateState(this);


        // Creates a Raycast and checks wether or not you hover above the commandCenter
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        int layerMask = 1 << 6;

        bool raycastHit = Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);

        // Applies a shader if you hover above the commandCenter
        if (raycastHit)
        {
            commandCenterObject.layer = LayerMask.NameToLayer("Outline");
            hoversAbove = true;
        }
        else
        {
            commandCenterObject.layer = LayerMask.NameToLayer("Default");
            hoversAbove = false;
        }


        if (hoversAbove)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                SwitchStates(clickedState);
            }
        }
    }

    public void SwitchStates(CommandCenterBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Worker"))
        {
            UnitStateManager unitStateManager = other.GetComponent<UnitStateManager>();

            if (unitStateManager != null) // Check if the component was found
            {
                collectedCompleteEnergy = unitStateManager.collectedEnergy;  // Access the collectedEnergy from the other GameObject

                unitStateManager.collectedEnergy = 0; // Reset the collectedEnergy on that GameObject
            }
            else
            {
                Debug.LogError("UnitStateManager component not found on the Worker!");
            }
        }
    }

}
