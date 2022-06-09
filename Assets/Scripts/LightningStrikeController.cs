using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrikeController : MonoBehaviour
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
    GameObject lightningStrike = objectManager.MakeObj("LightningStrike");
    LightningStrike lightningStrikeLogic = lightningStrike.GetComponent<LightningStrike>();
    lightningStrikeLogic.gameManager = gameManager;
    int randomRangeY = (int)(camera.orthographicSize - 1);
    int randomRangeX = (int)(randomRangeY * camera.aspect - 1);
    int randomPosX = Random.Range(-randomRangeX, randomRangeX);
    int randomPosY = Random.Range(-randomRangeY, randomRangeY);
    lightningStrike.transform.position = new Vector3(player.position.x + randomPosX, player.position.y + randomPosY, 0);
    // lightningStrike.transform.position = new Vector3(player.position.x + randomPosX, player.position.y + randomPosY, 0);

    curShotDelay = 0;
  }

  void Reload()
  {
    curShotDelay += Time.deltaTime;
  }
}
