using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using UnityEngine;

public class DirectionChanger : MonoBehaviour
{
    public string enemyTag = "Enemy";
    public Transform targetDirection;
    public Vector3 direction;
    public Quaternion rotation;

    private bool isEnemyInRange = false;
    void Start()
    {
        var calculatedDirection = targetDirection.position - transform.position;
        var normalizedDirection = calculatedDirection.normalized;
        direction = normalizedDirection;

        rotation = Quaternion.LookRotation(direction);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            isEnemyInRange = true;
            Debug.Log("ENEMY!");
            var enemyMovementScript = other.GetComponent<EnemyController>();
            if (enemyMovementScript != null)
            {
                enemyMovementScript.movementDirection = direction;
                enemyMovementScript.ChangeRotation(rotation);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(enemyTag))
            isEnemyInRange = false;
    }

    void Update()
    {

    }
}
