using System;
using System.Collections;
using UnityEngine;

public class EnemyCommandCenter : MonoBehaviour
{
    [SerializeField] GameObject unitPrefab;
    [SerializeField] Material blueSpaceMarine;
    private Vector3 spawnLocation;
    private void Start()
    {
        spawnLocation = new Vector3(-43, 3.26f, 35);
        StartCoroutine(WaitForSeconds(1));
    }

    private IEnumerator WaitForSeconds(int waitTime)
    {
       yield return new WaitForSeconds(waitTime);
       SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        System.Random _rnd = new System.Random();
        int randomNumber = _rnd.Next(1, 4);
        // Debug.Log("ShouldSpawn: " + randomNumber.ToString());
        for (int i = 1; i <= randomNumber; i++)
        {
            InstantiateMarine();

            if (i == randomNumber)
            {
                int gulp = _rnd.Next(5, 15);
                // Debug.Log("Should Wait: " + gulp.ToString());
                StartCoroutine(WaitForSeconds(gulp));
            }
        }
    }

    void InstantiateMarine()
    {
        float spacing = 4.0f;
        Vector3 offset = new Vector3(UnityEngine.Random.Range(-spacing, spacing), 0, UnityEngine.Random.Range(-spacing, spacing));
        GameObject obj = Instantiate(unitPrefab, spawnLocation + offset, Quaternion.identity);
        UnitStateManager _unit = obj.GetComponent<UnitStateManager>();
        _unit.myEnemyTag = "Player";
        _unit.life = 50;
        _unit.damage = 5;
        obj.GetComponentInChildren<SkinnedMeshRenderer>().material = blueSpaceMarine;
        obj.AddComponent<BasicEnemyAI>();
        obj.transform.SetParent(GameObject.FindGameObjectWithTag("EnemyContainer").transform);
        obj.transform.localScale = new Vector3(0.58f, 0.58f, 0.58f);
        obj.tag = "Enemy";
    }

}
