using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrow : MonoBehaviour
{
  public int dmg;
  public GameObject explosion;
  public GameManager gameManager;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "BulletBorder" || other.gameObject.tag == "Enemy")
    {
      gameManager.CallExplosion(transform.position);
      gameObject.SetActive(false);
    }
  }

}
