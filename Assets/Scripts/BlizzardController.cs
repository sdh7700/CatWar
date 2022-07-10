using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlizzardController : MonoBehaviour
{
  public ObjectManager objectManager;
  public GameManager gameManager;

  public Transform player;
  public Camera camera;

  public float maxShotDelay;
  public float curShotDelay;

  // Update is called once per frame
  void Update()
  {
    Fire();
    Reload();
  }

  void Fire()
  {
    if (curShotDelay < maxShotDelay)
      return;
    GameObject blizzard = objectManager.MakeObj("Blizzard");
    int randomRangeY = (int)(camera.orthographicSize - 1);
    int randomRangeX = (int)(randomRangeY * camera.aspect - 1);
    int randomPosX = Random.Range(-randomRangeX, randomRangeX);
    int randomPosY = Random.Range(-randomRangeY, randomRangeY);
    blizzard.transform.position = new Vector3(player.position.x + randomPosX - 5, player.position.y + randomPosY + 15, 0);

    curShotDelay = 0;
  }

  void Reload()
  {
    curShotDelay += Time.deltaTime;
  }
}
