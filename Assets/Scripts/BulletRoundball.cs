using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRoundball : MonoBehaviour
{
  public Transform player;
  public GameManager gameManager;
  public float bulletSpeed;
  public int dmg;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    transform.RotateAround(player.position, Vector3.back, bulletSpeed * Time.deltaTime);
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Enemy")
    {
      gameManager.CallExplosion(transform.position);
    }
  }
}
