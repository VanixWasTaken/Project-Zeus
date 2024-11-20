using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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
    [SerializeField] TextMeshProUGUI energyMeter;
    #endregion

    void Start()
    {
        GameDataManager.Instance.currentEnergy = GameDataManager.Instance.maxEnergy;
        energyMeter.text = ("Energy:\t" +  GameDataManager.Instance.currentEnergy + " / " + GameDataManager.Instance.maxEnergy);
        currentState = idleState;
        currentState.EnterState(this);
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
    }

    void UpdateEnergyMeter()
    {
        energyMeter.text = ("Energy:\t" + GameDataManager.Instance.currentEnergy + " / " + GameDataManager.Instance.maxEnergy);
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

    public void DepleteEnergy(int _amount)
    {
        int difference = GameDataManager.Instance.currentEnergy - _amount;
        ExitCheck(difference, _amount);
    }
}
