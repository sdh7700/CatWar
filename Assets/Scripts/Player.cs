using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Joystick joystick;

    public float speed;
    public float bulletSpeed;
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
        //playerAnim.SetInteger("Input", 1);
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            playerAnim.SetInteger("Input", 1);
            // if (Input.GetButton("Fire1"))

            // Change Direction
            spriteRenderer.flipX = joystick.Horizontal < 0;
            Debug.Log("moving");

            MoveControl();
        }
        else
        {
            Debug.Log("not moving");
            playerAnim.SetInteger("Input", 0);
        }
        Move();
        //Fire();
        //Reload();
    }

    private void MoveControl()
    {
        Vector3 upMovement = Vector3.up * speed * Time.deltaTime * joystick.Vertical;
        Vector3 rightMovement = Vector3.right * speed * Time.deltaTime * joystick.Horizontal;
        //Vector3 movement = (upMovement + rightMovement).normalized;
        //transform.position += movement;
        transform.position += upMovement;
        transform.position += rightMovement;
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
            spriteRenderer.flipX = h != 1;
        }

        // Walking Animation
        // if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        //     playerAnim.SetInteger("Input", 1);
        // else
        //     playerAnim.SetInteger("Input", 0);
    }

    void Fire()
    {
        if (!Input.GetButton("Fire1"))
            return;
        if (curShotDelay < maxShotDelay)
            return;

        GameObject bullet = objectManager.MakeObj("PlasmaBullet");

        Rigidbody2D rigidBullet = bullet.GetComponent<Rigidbody2D>();
        if (spriteRenderer.flipX == true)
        {
            bullet.transform.position = transform.position + new Vector3(-1, 0, 0);
            rigidBullet.AddForce(Vector2.left * bulletSpeed, ForceMode2D.Impulse);
        }
        else
        {
            bullet.transform.position = transform.position + new Vector3(1, 0, 0);
            rigidBullet.AddForce(Vector2.right * bulletSpeed, ForceMode2D.Impulse);
        }

        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
}
