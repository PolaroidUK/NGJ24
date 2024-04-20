using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public bool isDamaging;
    private SpriteRenderer sp;
    [SerializeField] private float speed = 4;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    public void Shoot(Vector2 vel)
    {
        GetComponent<Rigidbody2D>().velocity = vel * speed;
        
        sp.color = isDamaging ? Color.red : Color.green;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (isDamaging)
            {
                
                col.gameObject.GetComponent<PlayerController>().DealDamage();
            }
            else
            {
    
                col.gameObject.GetComponent<PlayerController>().HealDamage();
            }

            Destroy(gameObject);
        }

        isDamaging = !isDamaging;
        sp.color = isDamaging ? Color.red : Color.green;
        //Vector2 inDirection = GetComponent<Rigidbody2D>().velocity;
        //Vector2 inNormal = col.contacts[0].normal;
        //Vector2 newVelocity = Vector2.Reflect(inDirection, inNormal) * speed;
        //GetComponent<Rigidbody2D>().velocity = newVelocity;
    }
}
