using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public GameObject player;

  public int maxHealth;
  public int health;
  public float maxSpeed;
  public float speed;
  public float withinRange;

  SpriteRenderer spriteRenderer;
  Animator enemyAnim;
  BoxCollider2D boxCollider2D;

  void Awake()
  {
    health = maxHealth;
    speed = maxSpeed;
    spriteRenderer = GetComponent<SpriteRenderer>();
    enemyAnim = GetComponent<Animator>();
    boxCollider2D = GetComponent<BoxCollider2D>();
  }

  // Update is called once per frame
  void Update()
  {
    // Get the distance between enemy and player
    float dist = Vector3.Distance(player.transform.position, transform.position);
    // check if it is within the range you set
    if (dist <= withinRange)
    {
      // move to target
      transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
      spriteRenderer.flipX = transform.position.x > player.transform.position.x;
      if (transform.position.y < player.transform.position.y)
        spriteRenderer.sortingOrder = 3;
      else
        spriteRenderer.sortingOrder = 2;
    }
  }

  void OnHit(int dmg)
  {
    health -= dmg;
    enemyAnim.SetInteger("Hit", 1);
    Invoke("ReturnSprite", 0.2f);
    if (health < 0)
    {
      enemyAnim.SetTrigger("Die");
      boxCollider2D.enabled = false;
      speed = 0;
      Invoke("EnemyDie", 0.8f);

      //gameObject.SetActive(false);
    }
  }

  void ReturnSprite()
  {
    enemyAnim.SetInteger("Hit", 0);
  }

  void EnemyDie()
  {
    gameObject.SetActive(false);
    boxCollider2D.enabled = true;
    speed = maxSpeed;
    health = maxHealth;
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "PlayerBullet")
    {
      Bullet bullet = other.gameObject.GetComponent<Bullet>();
      OnHit(bullet.dmg);
      //other.gameObject.SetActive(false);
    }
  }
}
