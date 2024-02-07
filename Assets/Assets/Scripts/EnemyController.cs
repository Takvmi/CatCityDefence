using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector3 movementDirection;
    public float movementSpeed;
    public float health;
    public int damage;
    public float healthSpacing = 1f;
    public GameObject body;
    public GameObject healthBar;
    public GameObject healthOrbPrefab;

    private Camera playerCamera;

    void Start()
    {
        CreateHealthBar();
        playerCamera = Camera.main;
    }


    void Update()
    {
        var movement = movementDirection.normalized * movementSpeed * Time.deltaTime;
        transform.Translate(movement);
        FaceHealthBar();
        
        if(!GameManager.Instance.GameGoing)
            DestroyEnemy();
    }

    private void CreateHealthBar()
    {
        healthBar.transform.rotation = Quaternion.identity;
        var totalWidth = (health - 1) * healthSpacing;
        var startPosition = healthBar.transform.position - new Vector3(totalWidth / 2f, 0f, 0f);
        for (var i = 0; i < health; i++)
        {
            var spawnPosition = startPosition + new Vector3(i * healthSpacing, 0f, 0f);
            Instantiate(healthOrbPrefab, spawnPosition, Quaternion.identity, healthBar.transform);
        }
    }

    private void RefreshHealthBar()
    {
        foreach (Transform child in healthBar.transform)
            Destroy(child.gameObject);
        CreateHealthBar();
    }

    private void FaceHealthBar()
    {
        var lookAtDirection = playerCamera.transform.position - healthBar.transform.position;
        lookAtDirection.y = 0;
        healthBar.transform.rotation = Quaternion.LookRotation(lookAtDirection);
    }

    public void ChangeRotation(Quaternion rotation)
    {
        body.transform.rotation = rotation;
    }

    public void DamageEnemy(int damage)
    {
        if (!(damage < health)) DestroyEnemy();

        health -= damage;
        for (var i = 0; i < damage; i++)
        {
            DeleteHealthOrb();
            RefreshHealthBar();
        }
    }

    private void DeleteHealthOrb()
    {
        var remainingHealthOrbs = healthBar.transform.childCount;
        
        if (remainingHealthOrbs > 0)
        {
            var lastHealthOrb = healthBar.transform.GetChild(remainingHealthOrbs - 1);
            Destroy(lastHealthOrb.gameObject);
            RefreshHealthBar();
        }
    }

    public void DestroyEnemy()
    {
        GameManager.Instance.enemiesList.Remove(this.GameObject());
        Destroy(body);
        Destroy(this.gameObject);
    }
}
