using Unity.VisualScripting;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    public GameObject body;
    public GameObject bulletPrefab;
    public GameObject rangeArea;
    public Transform bulletSpawnPoint;
    public float range;
    public float attackSpeed;
    public int bulletSpeed;
    public int damage;
    public float bulletLifespan;

    private GameObject enemyTarget;
    private float nextShotTime;
    
    void Start()
    {
        rangeArea.transform.localScale = new Vector3(range * 10,range * 10,range * 10);
        nextShotTime = Time.time;
    }
    
    void Update()
    {
        if (enemyTarget != null && Vector3.Distance(transform.position, enemyTarget.transform.position) <= range)
        {
            FaceEnemy();
            Shoot();
        }
        else TargetEnemy();
    }

    private void Shoot()
    {
        if (!(Time.time >= nextShotTime)) return;
        
        var direction = body.transform.forward;
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.identity);
        bullet.GetComponent<BaseBullet>().SetupBullet(damage, bulletLifespan);
        var bulletBody = bullet.GetComponent<Rigidbody>();
        if (bulletBody != null)
            bulletBody.velocity = direction * bulletSpeed;

        nextShotTime = Time.time + 1f / attackSpeed;
    }

    private void TargetEnemy()
    {
        GameObject nearestEnemy = null;
        var nearestEnemyDistance = Mathf.Infinity;

        foreach (var enemy in GameManager.Instance.enemiesList)
        {
            var distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (!(distance <= range) || !(distance < nearestEnemyDistance)) continue;
            nearestEnemy = enemy;
            nearestEnemyDistance = distance;
        }

        enemyTarget = nearestEnemy;
    }
    
    private void FaceEnemy()
    {
        
        var lookAtDirection = enemyTarget.transform.position - transform.position;
        //lookAtDirection.y = 0;
        body.transform.rotation = Quaternion.LookRotation(lookAtDirection);
    }
}
