using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public bool isDamaging;
    private SpriteRenderer sp;
    [SerializeField] private float speed = 10;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    public void Shoot(Vector2 vel, bool b)
    {
        GetComponent<Rigidbody2D>().velocity = vel * speed;
        isDamaging = b;
        sp = GetComponent<SpriteRenderer>();
        sp.color = isDamaging ? Color.red : Color.green;

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isDamaging)
        {
            col.gameObject.GetComponent<PlayerController>().DealDamage();
            // When a ball hits a player, it makes a big particle effect
            Explode(bigExplosion);
        }
        else
        {
            col.gameObject.GetComponent<PlayerController>().HealDamage();
            // When a ball hits a player, it makes a big particle effect
            Explode(bigExplosion);
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        isDamaging = !isDamaging;
        sp.color = isDamaging ? Color.red : Color.green;
        //Vector2 inDirection = GetComponent<Rigidbody2D>().velocity;
        //Vector2 inNormal = col.contacts[0].normal;
        //Vector2 newVelocity = Vector2.Reflect(inDirection, inNormal) * speed;
        //GetComponent<Rigidbody2D>().velocity = newVelocity;


        // When a ball hits a barrier or another ball, it makes a small particle effect
        Explode(smallExplosion);

    }

    public GameObject smallExplosion;
    public GameObject bigExplosion;

    void Explode(GameObject smallOrBigExplosion)
    {
        GameObject explosion;
        explosion = Instantiate(smallOrBigExplosion, transform.position, Quaternion.identity);
        //        explosion.GetComponent<ParticleSystem>().Play();
        Destroy(explosion, .8f);
    }
}
