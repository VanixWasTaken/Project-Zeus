using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CommandCenterStateManager : MonoBehaviour
{
    
    #region CommandCenter States

    CommandCenterBaseState currentState;
    public CommandCenterIdleState idleState = new CommandCenterIdleState();
    public CommandCenterClickedState clickedState = new CommandCenterClickedState();

    #endregion

    #region References

    private InputActions inputActions;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject commandCenterObject;
    [SerializeField] TextMeshProUGUI energyMeter;
    [SerializeField] UnitStateManager unit;
    public GameObject buildingButton;
    public GameObject playerButton;
    public GameObject extracttionUnitInfo;
    public TextMeshProUGUI extractionUIWorkers;
    public TextMeshProUGUI extractionUIRecons;
    public TextMeshProUGUI extractionUIGatherers;
    public TextMeshProUGUI gatheredLootUI;
    public Button extractionUIExtractButton;
    public GameObject extractionWarningMenu;

    #endregion

    #region Variables

    public bool hoversAbove;
    public int collectedCompleteEnergy;
    public int workersInsideExtraction;
    public int reconsInsideExtraction;
    public int gatherersInsideExtraction;
    bool allUnitsInsideExtraction;

    #endregion


    #region Unity Build In

    private void Start()
    {
        SetData();

        currentState = idleState;
        currentState.EnterState(this);

        inputActions = new InputActions();
        inputActions.Mouse.Enable();
    }

    private void Update()
    {
        currentState.UpdateState(this);

        UpdateEnergyMeter();
        UpdateLoot();

        MouseHoverShader(); // Check if the mouse hovers above the Object, if yes it applies a shader. And if you press leftclick while hovering it changes states.
    }

    #region Colliders

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Worker"))
        {
            Debug.Log("I am inside the drop zone");
            workersInsideExtraction++;

            UnitStateManager unitStateManager = other.GetComponent<UnitStateManager>();

            if (unitStateManager != null) // Check if the component was found
            {
                Debug.Log("I am delivering my energy, which is: " + unitStateManager.collectedEnergy);
                // this block checks if the current energy is greater than the max and adjusts the values accordingly
                int checkEnergy = GameDataManager.Instance.currentEnergy + unitStateManager.collectedEnergy;

                if (checkEnergy <= GameDataManager.Instance.maxEnergy)
                {
                    GameDataManager.Instance.currentEnergy = checkEnergy;
                }
                else if (checkEnergy > GameDataManager.Instance.maxEnergy)
                {
                    GameDataManager.Instance.currentEnergy = GameDataManager.Instance.maxEnergy;
                }
                else
                {
                    Debug.LogError("Error! Calculating energy failed!");
                }

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

            UnitStateManager unitStateManager = other.GetComponent<UnitStateManager>();
            GathererLoot lootOnUnit = other.GetComponent<GathererLoot>();

            if (unitStateManager != null) // Check if the component was found
            {
                if (unitStateManager.collectedLoot > 0)
                {
                    GameDataManager.Instance.collectedLoot++;
                }

                if (unitStateManager.collectedLoot > 0)
                {
                    unitStateManager.collectedLoot--;
                }

                lootOnUnit.lootGO.SetActive(false);
            }
            else
            {
                Debug.LogError("UnitStateManager component not found on the Gatherer!");
            }
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

    #endregion

    #region Buttons

    public void OnExtractionButtonClicked()
    {
        AllUnitsInsideExitCheck();

        if (allUnitsInsideExtraction)
        {
            extractionWarningMenu.SetActive(false);
            GameDataManager.Instance.extractedWorkers = workersInsideExtraction;
            GameDataManager.Instance.extractedRecons = reconsInsideExtraction;
            GameDataManager.Instance.extractedGatherers = gatherersInsideExtraction;
            SceneManager.LoadScene("ExtractionScreenMenu");
        }
        else
        {
            extractionWarningMenu.SetActive(true);
        }
    }

    public void OnExtractionWarningNoClicked()
    {
        extracttionUnitInfo.SetActive(false);
    }

    public void OnExtractionWarningYesClicked()
    {
        GameDataManager.Instance.extractedWorkers = workersInsideExtraction;
        GameDataManager.Instance.extractedRecons = reconsInsideExtraction;
        GameDataManager.Instance.extractedGatherers = gatherersInsideExtraction;
        SceneManager.LoadScene("ExtractionScreenMenu");
    }

    #endregion

    #endregion


    #region Custom Functions()

    public void SwitchStates(CommandCenterBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void DepleteEnergy(int _amount)
    {
        int difference = GameDataManager.Instance.currentEnergy - _amount;
        ExitCheck(difference, _amount);
    }

    private void UpdateEnergyMeter()
    {
        energyMeter.text = ("Energy:\t" + GameDataManager.Instance.currentEnergy + " / " + GameDataManager.Instance.maxEnergy);
    }

    private void UpdateLoot()
    {
        gatheredLootUI.text = "Gathered Loot: " + GameDataManager.Instance.collectedLoot;
    }

    private void AllUnitsInsideExitCheck()
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

    private void ExitCheck(int _difference, int _amount)
    {
        if (_difference <= 0)
        {
            SceneManager.LoadScene("DeathScreenMenu");
        }
        else if (_difference > 0)
        {
            GameDataManager.Instance.currentEnergy -= _amount;
            UpdateEnergyMeter();
        }
    }

    private void SetData()
    {
        GameDataManager.Instance.currentEnergy = GameDataManager.Instance.maxEnergy;
        energyMeter.text = ("Energy:\t" + GameDataManager.Instance.currentEnergy + " / " + GameDataManager.Instance.maxEnergy);
    }

    private void MouseHoverShader()
    {
        // Creates a Raycast and checks wether or not you hover above the commandCenter
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        int layerMask = 1 << 6; // Check that the Layer is "Outline"

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

    #endregion
}
