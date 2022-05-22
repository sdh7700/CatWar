using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    public float curSpawnDelay;

    public ObjectManager objectManager;
    public GameObject player;
    Player playerLogic;

    public GameObject gameoverImage;

    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake()
    {
        playerLogic = player.GetComponent<Player>();
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
        GameOverCheck();
    }

    void SpawnEnemy()
    {
        int randomPoint = Random.Range(0, 4);
        GameObject enemy = objectManager.MakeObj("Ghost");
        enemy.transform.position = spawnPoints[randomPoint].position;
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
    }

    public void CallExplosion(Vector3 pos)
    {
        GameObject explosion = objectManager.MakeObj("Explosion");
        Explosion explosionLogic = explosion.GetComponent<Explosion>();

        explosion.transform.position = pos;
        explosionLogic.StartExplosion();
    }

    void GameOverCheck()
    {
        if (playerLogic.HP <= 0)
        {
            gameoverImage.SetActive(true);
        }
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }
}
