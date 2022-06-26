using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : MonoBehaviour
{
  public GameManager gameManager;

  public int dmg;
  // Start is called before the first frame update

  void OnEnable()
  {
    Invoke("OffLightningEffect", 0.5f);
  }
  // Update is called once per frame
  void Update()
  {

  }

  void OffLightningEffect()
  {
    gameObject.SetActive(false);
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Enemy")
    {
      gameManager.CallLightningExplosion(other.transform.position);
    }
  }

}
