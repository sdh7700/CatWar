using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
  public GameObject plasmaBulletPrefab;
  public GameObject ghostPrefab;

  GameObject[] ghost;
  GameObject[] plasmaBullet;

  GameObject[] targetPool;

  void Awake()
  {
    // enemy
    ghost = new GameObject[100];

    // bullet
    plasmaBullet = new GameObject[100];

    Generate();
  }

  void Generate()
  {
    // #1. Enemy
    for (int i = 0; i < ghost.Length; i++)
    {
      ghost[i] = Instantiate(ghostPrefab);
      ghost[i].SetActive(false);
    }

    // #2. Bullet
    for (int i = 0; i < plasmaBullet.Length; i++)
    {
      plasmaBullet[i] = Instantiate(plasmaBulletPrefab);
      plasmaBullet[i].SetActive(false);
    }
  }

  public GameObject MakeObj(string type)
  {


    switch (type)
    {
      case "Ghost":
        targetPool = ghost;
        break;
      case "PlasmaBullet":
        targetPool = plasmaBullet;
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
