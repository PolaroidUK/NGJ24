using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    
    private Vector3 moveInput;
    public float moveSpeed = 5f; 
    private Rigidbody2D rb;
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private Vector2 lookDirection;
    [SerializeField] private Transform pointer;
    [SerializeField] private Transform pointerPosition;
    
    [SerializeField] private GameObject shot;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        ConstraintCheck();
        PointerMove();
    }

    void OnMove(InputValue iv)
    {
        Vector2 inputVector = iv.Get<Vector2>();
        moveDirection = inputVector.normalized;
    }
    void OnLook(InputValue iv)
    {
        Vector2 inputVector = iv.Get<Vector2>();
        if (inputVector != Vector2.zero)
            lookDirection = inputVector.normalized;
    }

    public void OnFire()
    {
        Instantiate(shot,pointerPosition.position, quaternion.identity).GetComponent<Shot>().Shoot(lookDirection);
    }
    void Move()
    {
        rb.velocity = moveDirection * (moveSpeed );
    }
    
    private void ConstraintCheck()
    {
        if (transform.position.y > 4)
        {
            Vector3 pos = transform.position;
            pos.y = 4;
            transform.position = pos;
        }
        if (transform.position.y < -4)
        {
            Vector3 pos = transform.position;
            pos.y = -4;
            transform.position = pos;
        }
        if (transform.position.x > 8)
        {
            Vector3 pos = transform.position;
            pos.x = 8;
            transform.position = pos;
        }
        if (transform.position.x < -8)
        {
            Vector3 pos = transform.position;
            pos.x = -8;
            transform.position = pos;
        }
    }
    private void PointerMove()
    {
        pointer.rotation = quaternion.AxisAngle(Vector3.forward,Mathf.Atan2(lookDirection.y,lookDirection.x));
    }
}