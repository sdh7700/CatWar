using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOrb : MonoBehaviour
{
    public ObjectManager objectManager;
    public Transform playerTransform;

    public float maxShotDelay;
    public float curShotDelay;
    public float bulletSpeed;

    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        Reload();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (Input.GetButton("Horizontal"))
        {
            if (h >= 0)
                transform.position = playerTransform.position + new Vector3(1.2f, 0, 0);
            else
                transform.position = playerTransform.position + new Vector3(-1.2f, 0, 0);
        }
    }

    void Fire()
    {
        if (curShotDelay < maxShotDelay)
            return;
        Enemy closestEnemy = null;
        closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            GameObject bullet = objectManager.MakeObj("NormalBullet");
            Rigidbody2D rigidBullet = bullet.GetComponent<Rigidbody2D>();
            bullet.transform.position = transform.position;
            Vector2 now = (closestEnemy.transform.position - transform.position).normalized;

            rigidBullet.AddForce(now * bulletSpeed, ForceMode2D.Impulse);
        }

        curShotDelay = 0;
    }

    // Find closest enemy
    Enemy FindClosestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        Enemy closestEnemy = null;
        Enemy[] allEnemies = GameObject.FindObjectsOfType<Enemy>();

        foreach (Enemy currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }

        return closestEnemy;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

}
