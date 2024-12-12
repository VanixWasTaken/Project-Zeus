using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    #region References

    [SerializeField] Material emissiveMaterial;
    [SerializeField] Renderer objectToChange;
    private InputActions inputActions;
    private Color color;

    #endregion


    #region Unity Build In

    private void Start()
    {
        emissiveMaterial = objectToChange.GetComponent<Renderer>().material;
        color = emissiveMaterial.GetColor("_EmissionColor");

        inputActions = new InputActions();
        inputActions.Mouse.Enable();
    }

   

    void Update()
    {
        if (inputActions.Mouse.Click.IsPressed())
        {
            emissiveMaterial.SetColor("_EmissionColor", color * 2f);
        }

    }

    #endregion

}
