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
  public GameObject playerHitEffect;
  public GameObject beamController;
  public GameObject lightningStrikeController;

  Player playerLogic;
  PlayerOrb playerOrbLogic;

  // UI
  public GameObject gameoverImage;
  public Slider expGage;
  public Text levelText;
  bool pause = true;

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

  // 적 소환
  void SpawnEnemy()
  {
    int randomPoint = UnityEngine.Random.Range(0, spawnPoints.Length);
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

  // 발사체 폭발효과
  public void CallExplosion(Vector3 pos)
  {
    GameObject explosion = objectManager.MakeObj("Explosion");
    Explosion explosionLogic = explosion.GetComponent<Explosion>();

    explosion.transform.position = pos;
    explosionLogic.StartExplosion();
  }

  // 적 에센스 획득(적 제거시)
  public void GetEnemyEssence(Transform enemyPosition, int essenceType, int essenceCount) // 적 위치, 에센스 종류, 에센스 개수
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

  // 가장 가까운 적 찾기
  public Enemy FindClosestEnemy()
  {
    float distanceToClosestEnemy = Mathf.Infinity;
    Enemy closestEnemy = null;
    Enemy[] allEnemies = GameObject.FindObjectsOfType<Enemy>();

    foreach (Enemy curEnemy in allEnemies)
    {
      float distanceToEnemy = (curEnemy.transform.position - player.transform.position).sqrMagnitude;
      if (distanceToEnemy < distanceToClosestEnemy)
      {
        distanceToClosestEnemy = distanceToEnemy;
        closestEnemy = curEnemy;
      }
    }

    return closestEnemy;
  }

  // 플레이어 경험치 획득
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
  // 플레이어 레벨업
  void PlayerLevelUp()
  {
    expGage.value = 0;
    playerLogic.curExp = 0;
    playerLogic.level++;
    //playerOrbLogic.maxShotDelay *= 0.9f;
    levelText.text = "Lv" + playerLogic.level;
    if (playerLogic.level == 2)
    {
      playerRoundBallA.SetActive(true);
    }
    if (playerLogic.level == 3)
    {
      playerRoundBallB.SetActive(true);
      playerRoundBallB.transform.localPosition = playerRoundBallA.transform.localPosition * (-1);
    }
    if (playerLogic.level == 4)
    {
      beamController.SetActive(true);
    }
    if (playerLogic.level == 5)
    {
      lightningStrikeController.SetActive(true);
    }
  }

  // 플레이어 피격 시 붉은화면 이펙트
  public void PlayerHitEffect()
  {
    playerHitEffect.SetActive(true);
    Invoke("PlayerHitEffectRemove", 0.8f);
  }
  // 붉은화면 제거
  void PlayerHitEffectRemove()
  {
    playerHitEffect.SetActive(false);
  }

  // 일시정지 관리
  void PauseManagement()
  {
    if (pause == false)
    {
      Time.timeScale = 1;
    }
    else
    {
      Time.timeScale = 0;
    }
  }

  // 게임오버
  void GameOver()
  {
    if (playerLogic.HP <= 0)
    {
      gameoverImage.SetActive(true);
    }
  }
  // 게임재시작
  public void GameRetry()
  {
    SceneManager.LoadScene(0);
  }

  // 게임종료
  void GameQuit()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      Application.Quit();
    }
  }
}
