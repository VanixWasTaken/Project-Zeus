using System;
using TMPro;
using UnityEditor.Animations;
using UnityEditor.UI;
using UnityEngine;

public class RightPartUIUnitDescription : MonoBehaviour
{

    GameObject textMeshParent;
    TextMeshProUGUI textMesh;
    TextMeshProUGUI[] textMeshArray;

    public int holdingItemsInt;
    public bool isActive = true;
    string onOff;

    string unitType;

    void Awake()
    {
        textMeshParent = GameObject.Find("RightPartUI");    

        textMeshArray = GetComponentsInChildren<TextMeshProUGUI>();
    }

    void Start()
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


    }



    void Update()
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

        textMesh.text = unitType + " : " + onOff + "Items: " + holdingItemsInt;
    }
}
