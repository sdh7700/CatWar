using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    public float curSpawnDelay;

    public ObjectManager objectManager;
    public GameObject player;
    public GameObject playerOrb;
    Player playerLogic;
    PlayerOrb playerOrbLogic;

    public GameObject gameoverImage;
    public Slider expGage;

    void Awake()
    {
        playerLogic = player.GetComponent<Player>();
        playerOrbLogic = playerOrb.GetComponent<PlayerOrb>();
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
        GameOver();
        GameQuit();
    }

    void SpawnEnemy()
    {
        int randomPoint = UnityEngine.Random.Range(0, 4);
        try
        {
            GameObject enemy = objectManager.MakeObj("Ghost");
            enemy.transform.position = spawnPoints[randomPoint].position;
            Enemy enemyLogic = enemy.GetComponent<Enemy>();
            enemyLogic.player = player;
            enemyLogic.gameManager = this;
        }
        catch (NullReferenceException nre)
        {
            Debug.Log(nre);
        }
    }

    public void CallExplosion(Vector3 pos)
    {
        GameObject explosion = objectManager.MakeObj("Explosion");
        Explosion explosionLogic = explosion.GetComponent<Explosion>();

        explosion.transform.position = pos;
        explosionLogic.StartExplosion();
    }

    public void PlayerExpUp(int enemyExp)
    {
        Debug.Log(enemyExp);
        playerLogic.curExp += enemyExp;
        expGage.value = (float)playerLogic.curExp / playerLogic.needExp[playerLogic.level];
        if (expGage.value >= 1 && playerLogic.level <= 10)
        {
            expGage.value = 0;
            playerLogic.curExp = 0;
            playerLogic.level++;
            playerOrbLogic.maxShotDelay *= 0.8f;
        }
    }

    void GameOver()
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

    void GameQuit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
