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
        Debug.Log("ShouldSpawn: " + randomNumber.ToString());
        for (int i = 1; i <= randomNumber; i++)
        {
            GameObject obj = Instantiate(unitPrefab, spawnLocation, Quaternion.identity);
            obj.GetComponent<UnitStateManager>().myEnemyTag = "Player";
            obj.GetComponentInChildren<SkinnedMeshRenderer>().material = blueSpaceMarine;
            obj.AddComponent<BasicEnemyAI>();
            obj.transform.SetParent(GameObject.FindGameObjectWithTag("EnemyContainer").transform);

            if (i == randomNumber)
            {
                int gulp = _rnd.Next(1, 8);
                Debug.Log("Should Wait: " + gulp.ToString());
                StartCoroutine(WaitForSeconds(gulp));
            }
        }
    }
}
