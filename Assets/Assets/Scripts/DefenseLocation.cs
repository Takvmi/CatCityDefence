using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseLocation : MonoBehaviour
{
    public string enemyTag = "Enemy";
    public int defenseHealth = 100;
    void Start()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            Debug.Log("ENEMY HIT!");
            var enemyMovementScript = other.GetComponent<EnemyController>();
            if (enemyMovementScript != null)
            {
                defenseHealth -= enemyMovementScript.damage;
                enemyMovementScript.DestroyEnemy();
            }
        }
    }


    void Update()
    {
        if (defenseHealth < 1)
        {
            Debug.Log("DEFEAT");
            GameManager.Instance.EndGame();
        }
    }
}
