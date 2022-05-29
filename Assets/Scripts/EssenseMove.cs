using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenseMove : MonoBehaviour
{
  public GameObject player;
  public GameManager gameManager;
  public float maxSpeed;
  public float speed;
  public int enemyExp;
  public Vector3 startMovePos;

  Rigidbody2D rigid;

  void Awake()
  {
    speed = maxSpeed;
    rigid = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    Vector3 playerPos = player.transform.position;
    transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
    speed += Time.deltaTime * 3;
    transform.position += startMovePos * Time.deltaTime * 3;
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      gameManager.PlayerExpUp(enemyExp);
      speed = maxSpeed;
      gameObject.SetActive(false);
    }
  }

}
