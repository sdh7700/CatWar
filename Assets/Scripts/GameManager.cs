using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
  // Enemy Spawn
  public Transform[] spawnPoints;
  public float maxSpawnDelay;
  public float curSpawnDelay;

  public ObjectManager objectManager;
  public GameObject player;
  public GameObject playerOrb;
  public GameObject playerRoundBallA;
  public GameObject playerRoundBallB;

  Player playerLogic;
  PlayerOrb playerOrbLogic;

  // UI
  public GameObject gameoverImage;
  public Slider expGage;
  public Text levelText;

  void Awake()
  {
    playerLogic = player.GetComponent<Player>();
    playerOrbLogic = playerOrb.GetComponent<PlayerOrb>();
    levelText.text = "Lv 1";
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

  public void GetEnemyEssence(Transform enemyPosition, int essenceType, int essenceCount)
  {
    float degree = Mathf.PI / essenceCount;
    Vector2[] movingPos = new Vector2[essenceCount];
    for (int i = 0; i < essenceCount; i++)
    {
      movingPos[i] = new Vector3(Mathf.Cos(i * degree), Mathf.Sin(i * degree), 0);
      GameObject enemyEssence = objectManager.MakeObj("EnemyEssence");
      enemyEssence.transform.position = enemyPosition.position;
      EssenseMove essenceMoveLogic = enemyEssence.GetComponent<EssenseMove>();
      essenceMoveLogic.player = player;
      essenceMoveLogic.gameManager = this;
      essenceMoveLogic.startMovePos = movingPos[i];
    }
  }

  public void PlayerExpUp(int enemyExp)
  {
    int maxLevel = playerLogic.needExp.Length - 1;
    playerLogic.curExp += enemyExp;
    expGage.value = (float)playerLogic.curExp / playerLogic.needExp[playerLogic.level];
    if (expGage.value >= 1 && playerLogic.level < maxLevel)
    {
      PlayerLevelUp();
    }
  }

  void PlayerLevelUp()
  {
    expGage.value = 0;
    playerLogic.curExp = 0;
    playerLogic.level++;
    playerOrbLogic.maxShotDelay *= 0.9f;
    levelText.text = "Lv" + playerLogic.level;
    if (playerLogic.level == 2)
    {
      playerRoundBallA.SetActive(true);
    }
    if (playerLogic.level == 3)
    {
      playerRoundBallB.SetActive(true);
      Debug.Log(playerRoundBallA.transform.position);
      Debug.Log(playerRoundBallA.transform.position * (-1));
      playerRoundBallB.transform.position = playerRoundBallA.transform.position * (-1);
      playerRoundBallB.transform.rotation = Quaternion.identity;
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
