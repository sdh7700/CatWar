using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public float speed;
  public float maxShotDelay;
  public float curShotDelay;

  public ObjectManager objectManager;
  Animator playerAnim;
  SpriteRenderer spriteRenderer;

  void Awake()
  {
    playerAnim = GetComponent<Animator>();
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
    float h = Input.GetAxisRaw("Horizontal");
    float v = Input.GetAxisRaw("Vertical");
    Vector3 curPos = transform.position;
    Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

    transform.position = curPos + nextPos;
    // Change Direction
    if (Input.GetButton("Horizontal"))
    {
      spriteRenderer.flipX = h == 1;
    }

    // Walking Animation
    if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
    {
      playerAnim.SetInteger("Input", Mathf.Abs((int)h));
    }
    if (Input.GetButtonDown("Vertical") || Input.GetButtonUp("Vertical"))
    {
      playerAnim.SetInteger("Input", Mathf.Abs((int)v));
    }
  }

  void Fire()
  {
    if (!Input.GetButton("Fire1"))
      return;
    if (curShotDelay < maxShotDelay)
      return;

    GameObject bullet = objectManager.MakeObj("PlasmaBullet");

    Rigidbody2D rigidBullet = bullet.GetComponent<Rigidbody2D>();
    if (spriteRenderer.flipX == false)
    {
      bullet.transform.position = transform.position + new Vector3(-1, 0, 0);
      rigidBullet.AddForce(Vector2.left * 10, ForceMode2D.Impulse);
    }
    else
    {
      bullet.transform.position = transform.position + new Vector3(1, 0, 0);
      rigidBullet.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
    }

    curShotDelay = 0;
  }

  void Reload()
  {
    curShotDelay += Time.deltaTime;
  }
}
