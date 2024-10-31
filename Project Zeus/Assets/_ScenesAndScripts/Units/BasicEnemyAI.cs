using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyAI : MonoBehaviour
{
    [SerializeField] bool isBase = false;
    [SerializeField] List<GameObject> playerUnits = new List<GameObject>();
    GameObject myEnemy;
    UnitStateManager stateManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!isBase)
        {
            stateManager = GetComponent<UnitStateManager>();
            UpdateList();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateList()
    {
        float spacing = 2;
        GameObject[] playerTags = GameObject.FindGameObjectsWithTag("Player");
        // Debug.Log(playerTags.Length);
        foreach (GameObject playerTag in playerTags)
        {
          playerUnits.Add(playerTag.gameObject);
        }
        System.Random _rnd = new System.Random();
        int randomListSpot = _rnd.Next(0, playerUnits.Count);
        myEnemy = playerUnits[randomListSpot];
        Vector3 offset = new Vector3(Random.Range(-spacing, spacing), 0, Random.Range(-spacing, spacing));
        stateManager.OnCommandMove(myEnemy.transform.position + offset);

    }

}
