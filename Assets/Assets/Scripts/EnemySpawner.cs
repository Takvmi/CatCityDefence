using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyZeroPrefab;
    public float spawnInterval = 2f;
    public int numberOfEnemiesToSpawn = 5;

    private int spawnedCount = 0;
    private float elapsedTime = 0f;
    
    void Start()
    {
        Debug.Log("TESTING GIT LINK");
        GameManager.Instance.StartGame();
    }


    void Update()
    {
        if (spawnedCount < numberOfEnemiesToSpawn)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= spawnInterval)
            {
                var newEnemy = SpawnEnemy(enemyZeroPrefab);
                GameManager.Instance.enemiesList.Add(newEnemy);
                var newEnemyController = newEnemy.GetComponent<EnemyController>();
                //newEnemyController.health = 4;
               // newEnemyController.movementSpeed = 0.8f;
                elapsedTime = 0f;
            }
        }
    }

    private GameObject SpawnEnemy(GameObject enemyToSpawn)
    {
        spawnedCount++;
        return Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
    }
}
