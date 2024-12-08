using TMPro;
using UnityEngine;

public class RightPartUIUnitDescription : MonoBehaviour
{

    #region References

    private GameObject textMeshParent;
    private TextMeshProUGUI textMesh;
    private TextMeshProUGUI[] textMeshArray;

    #endregion

    #region Variables

    public int holdingItemsInt;
    public bool isActive = true;
    private string onOff;
    private string unitType;

    #endregion



    #region Unity Build In

    private void Awake()
    {
        textMeshParent = GameObject.Find("RightPartUI");
    }


    private void Start()
    {
        if (gameObject.tag == "Worker")
        {
            unitType = "Worker";
        }
        else if (gameObject.tag == "Recon")
        {
            unitType = "Recon";
        }
        else if (gameObject.tag == "Gatherer")
        {
            unitType = "Gatherer";
        }

        textMeshArray = textMeshParent.GetComponentsInChildren<TextMeshProUGUI>();

    }


    private void Update()
    {
        if (isActive)
        {
            onOff = "On, ";
        }
        else
        {
            onOff = "Off, ";
        }

        foreach (TextMeshProUGUI child in textMeshArray)
        {
            if (child.text == "NotSet")
            {
                textMesh = child;
            }
        }

       // textMesh.text = unitType + " : " + onOff + "Items: " + holdingItemsInt;
    }

    #endregion
}
