using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  public int dmg;
  public ParticleSystem particleObject;
  public GameObject explosion;
  public GameManager gameManager;

  // Update is called once per frame
  void Update()
  {

  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "BulletBorder" || other.gameObject.tag == "Enemy")
    {
      gameManager.CallExplosion(transform.position);
      //particleObject.Stop();
      gameObject.SetActive(false);
    }
  }
}
