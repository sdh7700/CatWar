using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
  public GameManager gameManager;

  public int dmg;
  float rotateValue;

  // Update is called once per frame
  void Update()
  {
    Vector3 rotateVector = new Vector3(0, 0, Time.deltaTime * 80);
    transform.Rotate(rotateVector);
  }


  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Enemy")
    {
      gameManager.CallExplosion(other.ClosestPoint(transform.position));
    }
  }

}
