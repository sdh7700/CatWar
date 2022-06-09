using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
  public GameObject normalBulletPrefab;
  public GameObject plasmaBulletPrefab;
  public GameObject lightningStrikePrefab;

  public GameObject ghostPrefab;

  public GameObject explosionPrefab;
  public GameObject lightningExplosionPrefab;

  public GameObject enemyEssencePrefab;

  GameObject[] normalBullet;
  GameObject[] plasmaBullet;
  GameObject[] lightningStrike;

  GameObject[] ghost;

  GameObject[] targetPool;

  GameObject[] explosion;
  GameObject[] lightningExplosion;

  GameObject[] enemyEssence;

  void Awake()
  {
    // bullet
    normalBullet = new GameObject[100];
    plasmaBullet = new GameObject[100];
    lightningStrike = new GameObject[50];

    // enemy
    ghost = new GameObject[300];

    // Explosion
    explosion = new GameObject[100];
    lightningExplosion = new GameObject[100];

    // EnemyEssence
    enemyEssence = new GameObject[200];

    Generate();
  }

  void Generate()
  {
    // #1. Bullet
    for (int i = 0; i < normalBullet.Length; i++)
    {
      normalBullet[i] = Instantiate(normalBulletPrefab);
      normalBullet[i].SetActive(false);
    }
    for (int i = 0; i < plasmaBullet.Length; i++)
    {
      plasmaBullet[i] = Instantiate(plasmaBulletPrefab);
      plasmaBullet[i].SetActive(false);
    }
    for (int i = 0; i < lightningStrike.Length; i++)
    {
      lightningStrike[i] = Instantiate(lightningStrikePrefab);
      lightningStrike[i].SetActive(false);
    }

    // #2. Enemy
    for (int i = 0; i < ghost.Length; i++)
    {
      ghost[i] = Instantiate(ghostPrefab);
      ghost[i].SetActive(false);
    }

    // #3. Explosion
    for (int i = 0; i < explosion.Length; i++)
    {
      explosion[i] = Instantiate(explosionPrefab);
      explosion[i].SetActive(false);
    }
    for (int i = 0; i < lightningExplosion.Length; i++)
    {
      lightningExplosion[i] = Instantiate(lightningExplosionPrefab);
      lightningExplosion[i].SetActive(false);
    }

    // #4. EnemyEssence
    for (int i = 0; i < enemyEssence.Length; i++)
    {
      enemyEssence[i] = Instantiate(enemyEssencePrefab);
      enemyEssence[i].SetActive(false);
    }
  }

  public GameObject MakeObj(string type)
  {
    switch (type)
    {
      case "NormalBullet":
        targetPool = normalBullet;
        break;
      case "PlasmaBullet":
        targetPool = plasmaBullet;
        break;
      case "LightningStrike":
        targetPool = lightningStrike;
        break;
      case "Ghost":
        targetPool = ghost;
        break;
      case "Explosion":
        targetPool = explosion;
        break;
      case "LightningExplosion":
        targetPool = lightningExplosion;
        break;
      case "EnemyEssence":
        targetPool = enemyEssence;
        break;
    }

    for (int i = 0; i < targetPool.Length; i++)
    {
      if (!targetPool[i].activeSelf)
      {
        targetPool[i].SetActive(true);
        return targetPool[i];
      }
    }

    return null;
  }
}
