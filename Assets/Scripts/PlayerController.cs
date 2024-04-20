using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEditor.MPE;
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

    [SerializeField] private TextMeshPro textMesh;

    [SerializeField] private float leftLimit, rightLimit;



    GlobalEventManager _globalEventManager;
    [SerializeField] public int health = 3;
    [SerializeField] public int id;


    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingTime = 1f;
    [SerializeField] private float dashingCooldown = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _globalEventManager = GameManager.Instance.globalEventManager;
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
        //Vector2 inputVector = iv.Get<Vector2>();
        //if (inputVector != Vector2.zero)
        //lookDirection = inputVector.normalized;
    }

    void OnTestA(InputValue iv)
    {
        _globalEventManager.Dispatch(GlobalEventManager.EventTypes.Player1HealthDecrease, null);
        health -= 10;
    }
    void OnTestB(InputValue iv)
    {
        _globalEventManager.Dispatch(GlobalEventManager.EventTypes.Player1HealthIncrease, null);
        health += 10;
    }

    float timeFired;
    bool areDamaging = false;
    public void OnFire()
    {
        if (timeFired + 1 <= Time.time) // 1 second cooldown on shooting
        {
            GameObject newShot;
            var rotation = Quaternion.identity;
            rotation *= Quaternion.Euler(0, 0, -90); // this adds a 90 degrees Z rotation to place the triangle projectile in the right facing

            newShot = Instantiate(shot, pointerPosition.position, quaternion.identity);
            newShot.GetComponent<Shot>().Shoot(lookDirection, areDamaging);

            //            areDamaging = !areDamaging; // Each time you shoot, you will heal/harm
            timeFired = Time.time;
        }
    }

    // If we want to be able to shoot on the left trigger
    /*
        public void OnFire2()
        {
            Instantiate(shot, pointerPosition.position, quaternion.identity).GetComponent<Shot>().Shoot(lookDirection, true);
        }
    */
    void Move()
    {
        rb.velocity = moveDirection * (moveSpeed);
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
        if (transform.position.x > rightLimit)
        {
            Vector3 pos = transform.position;
            pos.x = rightLimit;
            transform.position = pos;
        }
        if (transform.position.x < leftLimit)
        {
            Vector3 pos = transform.position;
            pos.x = leftLimit;
            transform.position = pos;
        }
    }
    private void PointerMove()
    {
        pointer.rotation = quaternion.AxisAngle(Vector3.forward, Mathf.Atan2(lookDirection.y, lookDirection.x));
    }

    public void DealDamage()
    {
        //_globalEventManager.Dispatch(GlobalEventManager.EventTypes.Player1HealthDecrease, null);
        health -= 1;

        textMesh.text = health + "";
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void HealDamage()
    {
        //_globalEventManager.Dispatch(GlobalEventManager.EventTypes.Player1HealthIncrease, null);
        if (health >= 3)
        {
            return;
        }
        health += 1;

        textMesh.text = health + "";
    }

    public void Set(int i)
    {
        id = i;
        if (id == 1)
        {
            leftLimit = -8;
            rightLimit = 0;
            lookDirection.x = 1;
        }
        else
        {
            leftLimit = 0;
            rightLimit = 8;
            lookDirection.x = -1;
        }
    }

    private IEnumerator Dash()
    {
        if (canDash)
        {
            isDashing = true;
            canDash = false;
            moveSpeed = moveSpeed * dashingPower;
            yield return new WaitForSeconds(dashingTime);
            moveSpeed = moveSpeed / dashingPower;
            isDashing = false;
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
        }
    }

    private void OnDash()
    {
        Debug.Log("dashed");
        StartCoroutine(Dash());
    }
}
