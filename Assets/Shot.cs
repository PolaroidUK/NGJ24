using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public bool isDamaging;
    private SpriteRenderer sp;
    [SerializeField] FMODUnity.EventReference reflectSound;
    [SerializeField] private float speed = 10;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform spriteHolder;
    [SerializeField] private float rotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        spriteHolder.rotation = quaternion.AxisAngle(Vector3.forward, Mathf.Atan2(rb.velocity.y, rb.velocity.x) - rotation);
    }

    public void Shoot(Vector2 vel, bool b)
    {
        GetComponent<Rigidbody2D>().velocity = vel * speed;
        isDamaging = b;
        sp = GetComponentInChildren<SpriteRenderer>();
        sp.color = isDamaging ? Color.red : Color.green;

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isDamaging)
        {
            col.gameObject.GetComponent<PlayerController>().DealDamage();
            // When a ball damages a player, it makes a big particle effect
            Explode(bigExplosion);
        }
        else
        {
            col.gameObject.GetComponent<PlayerController>().HealDamage();
            // When a ball heals a player, it makes a healing particle effect
            //Explode(bigExplosion);
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        isDamaging = !isDamaging;
        sp.color = isDamaging ? Color.red : Color.green;
        GameManager.Instance.audioManager.playOneShot(reflectSound);
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
        Quaternion quaternion = Quaternion.identity;
        var randomRot = Quaternion.AngleAxis(UnityEngine.Random.Range(0, 360), Vector3.forward);
        explosion = Instantiate(smallOrBigExplosion, transform.position, randomRot);
        //        explosion.GetComponent<ParticleSystem>().Play();
        Destroy(explosion, .8f);
    }
}
