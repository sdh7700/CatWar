using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnife : MonoBehaviour
{
  public int turnSpeed;
  public int dmg;
  public GameManager gameManager;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    Vector3 rotVec = Vector3.forward;
    transform.Rotate(rotVec * turnSpeed);
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
