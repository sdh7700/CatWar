using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;

    public int health;
    public float speed;
    public float withinRange;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the distance between enemy and player
        float dist = Vector3.Distance(target.position, transform.position);
        // check if it is within the range you set
        if (dist <= withinRange)
        {
            // move to target
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            spriteRenderer.flipX = transform.position.x > target.position.x;
            if (transform.position.y < target.position.y)
                spriteRenderer.sortingOrder = 3;
            else
                spriteRenderer.sortingOrder = 2;
        }
    }

    void OnHit(int dmg)
    {
        health -= dmg;
    }
}
