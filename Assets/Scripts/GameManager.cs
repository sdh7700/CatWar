using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    public float curSpawnDelay;

    public ObjectManager objectManager;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        curSpawnDelay += Time.deltaTime;
        if (curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }
    }

    void SpawnEnemy()
    {
        int randomPoint = Random.Range(0, 4);
        GameObject enemy = objectManager.MakeObj("Ghost");
        enemy.transform.position = spawnPoints[randomPoint].position;
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
    }
}
