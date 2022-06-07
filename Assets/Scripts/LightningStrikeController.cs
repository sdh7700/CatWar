using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrikeController : MonoBehaviour
{
  public ObjectManager objectManager;

  public float maxShotDelay;
  public float curShotDelay;

  // Start is called before the first frame update
  void Start()
  {

  }

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
    float randomPosX = Random.Range(-10, 10);
    float randomPosY = Random.Range(-7, 7);
    lightningStrike.transform.localPosition = new Vector3(randomPosX, randomPosY, 0);
    Debug.Log(randomPosX + "," + randomPosY);

    curShotDelay = 0;
  }

  void Reload()
  {
    curShotDelay += Time.deltaTime;
  }
}
