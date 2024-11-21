using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public GameObject buildingButton;
    public GameObject playerButton;
    [SerializeField] UnitStateManager unit;
    InputActions inputActions;

    public GameObject extracttionUnitInfo;
    public TextMeshProUGUI extractionUIWorkers;
    public TextMeshProUGUI extractionUIRecons;
    public TextMeshProUGUI extractionUIGatherers;
    public Button extractionUIExtractButton;
    public GameObject extractionWarningMenu;
    #endregion


    public bool hoversAbove;
    public int collectedCompleteEnergy;
    public int workersInsideExtraction;
    public int reconsInsideExtraction;
    public int gatherersInsideExtraction;
    bool allUnitsInsideExtraction;



  


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
            workersInsideExtraction++;

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

        if (other.CompareTag("Recon"))
        {
            reconsInsideExtraction++;
        }
        
        if (other.CompareTag("Gatherer"))
        {
            gatherersInsideExtraction++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Worker"))
        {
            workersInsideExtraction--;
        }
        
        if (other.CompareTag("Recon"))
        {
            reconsInsideExtraction--;
        }

        if (other.CompareTag("Gatherer"))
        {
            gatherersInsideExtraction--;
        }
    }


    public void OnExtractionButtonClicked()
    {
        AllUnitsInsideExitCheck();

        if (allUnitsInsideExtraction)
        {
            extractionWarningMenu.SetActive(false);
            SceneManager.LoadScene("ExtractionScreenMenu");
        }
        else
        {
            extractionWarningMenu.SetActive(true);
        }
    }

    void AllUnitsInsideExitCheck()
    {
        if (GameDataManager.Instance.pickedWorkers + GameDataManager.Instance.pickedRecons + GameDataManager.Instance.pickedGatherers == workersInsideExtraction + reconsInsideExtraction + gatherersInsideExtraction)
        {
            allUnitsInsideExtraction = true;
        }
        else
        {
            allUnitsInsideExtraction = false;
        }
    }

    public void OnExtractionWarningNoClicked()
    {
        extracttionUnitInfo.SetActive(false);
    }

    public void OnExtractionWarningYesClicked()
    {
        SceneManager.LoadScene("ExtractionScreenMenu");
    }
}
