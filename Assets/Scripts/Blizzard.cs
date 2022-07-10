using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blizzard : MonoBehaviour
{
  public GameManager gameManager;
  Rigidbody2D rigidBody;
  BoxCollider2D boxCollider;
  Animator anim;

  public ParticleSystem particle;

  public float curBlizzardFallingTime;
  public float maxBlizzardFallingTime;
  bool isBlizzardFalling;
  public int dmg;

  // Start is called before the first frame update
  void Start()
  {

  }

  void Awake()
  {
    rigidBody = gameObject.GetComponent<Rigidbody2D>();
    boxCollider = gameObject.GetComponent<BoxCollider2D>();
    anim = gameObject.GetComponent<Animator>();
    isBlizzardFalling = true;
  }

  // Update is called once per frame
  void Update()
  {
    BlizzardFalling();
  }

  void BlizzardFalling()
  {
    if (isBlizzardFalling == false)
      return;
    if (curBlizzardFallingTime < maxBlizzardFallingTime)
    {
      Vector3 fallVector = new Vector3(0.15f, -0.6f, 0);
      gameObject.transform.position += fallVector;
    }
    else
    {
      isBlizzardFalling = false;
      BlizzardExplosion();
    }
    curBlizzardFallingTime += Time.deltaTime;
  }

  void BlizzardExplosion()
  {
    boxCollider.enabled = true;
    anim.SetBool("Explosion", true);
    Invoke("OffBlizzard", 0.5f);
  }

  void OffBlizzard()
  {
    boxCollider.enabled = false;
    anim.SetBool("Explosion", false);
    curBlizzardFallingTime = 0;
    isBlizzardFalling = true;
    gameObject.transform.position = new Vector3(-5, 10, 0);
    gameObject.SetActive(false);
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Enemy")
    {
      gameManager.CallExplosion(transform.position);
    }
  }
}
