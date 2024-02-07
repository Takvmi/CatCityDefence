using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public int damage;
    public float lifespan;

    private bool hit = false;

    private void Start()
    {
        Destroy(gameObject, lifespan);
    }

    public void SetupBullet(int dmg, float life)
    {
        damage = dmg;
        lifespan = life;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        hit = true;
        var enemyMovementScript = other.GetComponent<EnemyController>();
        if (enemyMovementScript != null)
            enemyMovementScript.DamageEnemy(damage);
            
        Destroy(gameObject);
    }

}
