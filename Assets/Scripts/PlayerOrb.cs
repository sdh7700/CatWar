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
  public float magicArrowSpeed;
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
    FireMagicArrow();
    Reload();
  }

  void Move()
  {
    if (joystick.Horizontal < 0)
      transform.position = playerTransform.position + new Vector3(-1.2f, 0, 0);
    else if (joystick.Horizontal > 0)
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

  // 일반 공격 발사
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

  // 매직애로우 발사
  void FireMagicArrow()
  {
    if (curArrowShotDelay < maxArrowShotDelay)
      return;

    float centerAngle = Mathf.Atan2(joystick.input.y, joystick.input.x) * Mathf.Rad2Deg;
    int startAngle = magicArrowCount % 2 == 0 ? (magicArrowCount / 2) * -10 + 5 + (int)centerAngle : (magicArrowCount / 2) * -10 + (int)centerAngle;
    for (int i = 0; i < magicArrowCount; i++)
    {
      GameObject magicArrow = objectManager.MakeObj("MagicArrow");
      Rigidbody2D rigidMagicArrow = magicArrow.GetComponent<Rigidbody2D>();
      magicArrow.transform.position = transform.position;
      magicArrow.transform.rotation = Quaternion.identity;

      int fireAngle = startAngle + i * 10;
      Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * fireAngle / 360), Mathf.Sin(Mathf.PI * 2 * fireAngle / 360));
      Vector3 rotVec = Vector3.forward * (fireAngle);
      magicArrow.transform.Rotate(rotVec);
      rigidMagicArrow.AddForce(dirVec.normalized * magicArrowSpeed, ForceMode2D.Impulse);
    }

    curArrowShotDelay = 0;
  }

  void ReturnColor()
  {
    spriteRenderer.color = new Color32(255, 255, 255, 255);
  }

  void Reload()
  {
    curShotDelay += Time.deltaTime;
    curArrowShotDelay += Time.deltaTime;
  }

}
