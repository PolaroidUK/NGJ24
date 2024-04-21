using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
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


    public GameObject attackPrefab;
    public GameObject healPrefab;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _globalEventManager = GameManager.Instance.globalEventManager;

        dashbarObject.SetActive(false);

        attackPrefab = transform.Find("AttackPrefab").gameObject;
        healPrefab = transform.Find("HealPrefab").gameObject;

        ShowAttack();
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
    bool areDamaging = true;
    public void OnFire()
    {
        if (timeFired + 1 <= Time.time) // 1 second cooldown on shooting
        {
            GameObject newShot;

            newShot = Instantiate(shot, pointerPosition.position, quaternion.identity);
            newShot.GetComponent<Shot>().Shoot(lookDirection, areDamaging);

            areDamaging = !areDamaging; // Each time you shoot, you will heal/harm
            timeFired = Time.time;

            Toggle();
        }
    }

    // If we want to be able to shoot on the left trigger
    /*
        public void OnFire2()
        {
            Instantiate(shot, pointerPosition.position, quaternion.identity).GetComponent<Shot>().Shoot(lookDirection, false);
        }*/

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
            //Destroy(gameObject);
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

    public void Set(int i) // This is for sorting the players
    {
        id = i;
        if (id == 0)
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

    public Material dashbar;

    public GameObject dashbarObject;

    public float dashCooldownCounter;

    private IEnumerator DashCooldown()
    {
        dashCooldownCounter = 0f;
        moveSpeed = moveSpeed * dashingPower;

        dashbar.SetFloat("_Dash", dashCooldownCounter);

        while (dashingTime >= 0)
        {
            dashingTime -= Time.deltaTime;
            //            yield return new WaitForSeconds(Time.deltaTime);
        }

        if (dashingTime <= 0)
        {
            moveSpeed = moveSpeed / dashingPower;
        }

        while (dashCooldownCounter <= dashingCooldown)
        {
            dashCooldownCounter += Time.deltaTime;
            dashbar.SetFloat("_Dash", dashCooldownCounter);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        dashCooldownCounter = 0f;
        canDash = true;
        isDashing = false;

        dashbarObject.SetActive(false);
    }


    private void OnDash()
    {
        if (canDash)
        {
            dashbarObject.SetActive(true);

            isDashing = true;
            canDash = false;

            StartCoroutine(DashCooldown());
        }
    }


    private bool showingAttack = true;

    // Call this method to toggle between prefabs
    public void Toggle()
    {
        if (showingAttack)
        {
            ShowHeal();
        }
        else
        {
            ShowAttack();
        }
    }

    void ShowAttack()
    {
        attackPrefab.SetActive(true);
        healPrefab.SetActive(false);
        showingAttack = true;
    }

    void ShowHeal()
    {
        attackPrefab.SetActive(false);
        healPrefab.SetActive(true);
        showingAttack = false;
    }

}
