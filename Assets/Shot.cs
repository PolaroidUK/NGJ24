using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public bool isDamaging;
    [SerializeField] private float speed = 4;
    
    public void Shoot(Vector2 vel)
    {
        GetComponent<Rigidbody2D>().velocity = vel*speed;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        
    }
}
