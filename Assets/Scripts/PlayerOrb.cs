using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOrb : MonoBehaviour
{
  public Joystick joystick;
  Vector3 nowDir; // 현재 바라보는 방향

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

  // Throwing Knife Status
  public float maxThrowingKnifeShotDelay;
  public float curThrowingKnifeShotDelay;
  public float throwingKnifeSpeed;
  public int maxThrowingKnifeCount;
  public int curThrowingKnifeCount;
  public float throwingKnifeFireGap;

  SpriteRenderer spriteRenderer;

  void Awake()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    nowDir = Vector3.zero;
  }

  // Update is called once per frame
  void Update()
  {
    Move();
    Fire();
    FireMagicArrow();
    FireThrowingKnifeController();
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

    // 조이스틱을 놓았을 때 발사방향이 원위치 되는 것을 방지하기 위해
    if (joystick.input != Vector3.zero)
      nowDir = joystick.input;

    float centerAngle = Mathf.Atan2(nowDir.y, nowDir.x) * Mathf.Rad2Deg;

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

  // 표창 발사
  void FireThrowingKnifeController()
  {
    // 발사 딜레이가 아직 안되었거나, 발사 중일 때 취소
    if ((curThrowingKnifeShotDelay < maxThrowingKnifeShotDelay) || curThrowingKnifeCount != 0)
      return;

    // 발사
    FireThrowingKnife();
    // 발사 끝
    Debug.Log("FireEnd");
  }
  void FireThrowingKnife()
  {
    // 발사 개수가 다 채워지면 개수 초기화 및 발사 딜레이 초기화 후 발사 종료
    if (curThrowingKnifeCount > maxThrowingKnifeCount)
    {
      curThrowingKnifeCount = 0;
      curThrowingKnifeShotDelay = 0;
      return;
    }
    // *발사*
    GameObject throwingKnife = objectManager.MakeObj("ThrowingKnife");
    Rigidbody2D rigidThrowingKnife = throwingKnife.GetComponent<Rigidbody2D>();
    throwingKnife.transform.position = transform.position;
    throwingKnife.transform.rotation = Quaternion.identity;
    int fireDirAngle = Random.Range(0, 361);
    Vector2 fireDir = new Vector2(Mathf.Cos(Mathf.PI * 2 * fireDirAngle / 360), Mathf.Sin(Mathf.PI * 2 * fireDirAngle / 360));
    rigidThrowingKnife.AddForce(fireDir.normalized * throwingKnifeSpeed, ForceMode2D.Impulse);
    // 발사 개수 카운트
    curThrowingKnifeCount++;
    // 추가 발사
    Invoke("FireThrowingKnife", throwingKnifeFireGap);
  }

  void ReturnColor()
  {
    spriteRenderer.color = new Color32(255, 255, 255, 255);
  }

  void Reload()
  {
    curShotDelay += Time.deltaTime;
    curArrowShotDelay += Time.deltaTime;
    curThrowingKnifeShotDelay += Time.deltaTime;
  }

}
