using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject normalBulletPrefab;
    public GameObject plasmaBulletPrefab;

    public GameObject ghostPrefab;

    GameObject[] normalBullet;
    GameObject[] plasmaBullet;

    GameObject[] ghost;

    GameObject[] targetPool;

    void Awake()
    {
        // bullet
        normalBullet = new GameObject[100];
        plasmaBullet = new GameObject[100];

        // enemy
        ghost = new GameObject[100];

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

        // #2. Enemy
        for (int i = 0; i < ghost.Length; i++)
        {
            ghost[i] = Instantiate(ghostPrefab);
            ghost[i].SetActive(false);
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
            case "Ghost":
                targetPool = ghost;
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
