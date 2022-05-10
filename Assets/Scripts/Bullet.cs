using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg;
    public ParticleSystem particleObject;

    void OnEnable()
    {
        //particleObject.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BulletBorder" || other.gameObject.tag == "Enemy")
        {
            particleObject.Stop();
            gameObject.SetActive(false);
        }
    }
}
