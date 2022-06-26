using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOrb : MonoBehaviour
{
  public Joystick joystick;

  public ObjectManager objectManager;
  public Transform playerTransform;
  public GameManager gameManager;

  // Normal Bullet Status
  public float maxShotDelay;
  public float curShotDelay;
  public float bulletSpeed;

  // Magic Arrow Status
  public float maxArrowShotDelay;
  public float curArrowShotDelay;
  public int magicArrowCount;

  SpriteRenderer spriteRenderer;

  void Awake()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  void Update()
  {
    Move();
    Fire();
    Reload();
  }

  void Move()
  {
    if (joystick.Horizontal < 0)
      transform.position = playerTransform.position + new Vector3(-1.2f, 0, 0);
    else
      transform.position = playerTransform.position + new Vector3(1.2f, 0, 0);
    // float h = Input.GetAxisRaw("Horizontal");
    // if (Input.GetButton("Horizontal"))
    // {
    //     if (h >= 0)
    //         transform.position = playerTransform.position + new Vector3(1.2f, 0, 0);
    //     else
    //         transform.position = playerTransform.position + new Vector3(-1.2f, 0, 0);
    // }
  }

  void Fire()
  {
    if (curShotDelay < maxShotDelay)
      return;
    Enemy closestEnemy = null;
    closestEnemy = gameManager.FindClosestEnemy();
    if (closestEnemy != null)
    {
      GameObject bullet = objectManager.MakeObj("NormalBullet");
      Bullet bulletLogic = bullet.GetComponent<Bullet>();
      Rigidbody2D rigidBullet = bullet.GetComponent<Rigidbody2D>();
      bulletLogic.gameManager = gameManager;
      bullet.transform.position = transform.position;
      Vector2 now = (closestEnemy.transform.position - transform.position).normalized;

      rigidBullet.AddForce(now * bulletSpeed, ForceMode2D.Impulse);
      spriteRenderer.color = new Color32(168, 168, 168, 255);
      Invoke("ReturnColor", 0.1f);
    }
    curShotDelay = 0;
  }

  void FireMagicArrow()
  {
    if (curArrowShotDelay < maxArrowShotDelay)
      return;
    GameObject bullet = objectManager.MakeObj("MagicArrow");
  }

  void ReturnColor()
  {
    spriteRenderer.color = new Color32(255, 255, 255, 255);
  }

  void Reload()
  {
    curShotDelay += Time.deltaTime;
  }

}
