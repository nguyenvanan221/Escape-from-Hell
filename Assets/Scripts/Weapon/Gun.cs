using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Gun : MonoBehaviour
{
    PlayerControls controls;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private AudioClip shootSound;
    private bool isFiring;

    [SerializeField] private float attackInterval;
    private float attackIntervalCountdown;

    public static float NumBullet {get; private set; }
    public float maxBullet;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        attackIntervalCountdown= 0;
        NumBullet = 0;

        controls = new();
        controls.Enable();
        controls.Land.Shoot.performed += ctx =>
        {
            if (NumBullet > 0f)
            {
                isFiring = true;
                Invoke("Shoot", 0.2f);
            }
        };
    }
    private void Update()
    {
        if (UIManager.Instance.isPause) { return; }
        if (PlayerMove.isClimbing || PlayerMove.isJumping) { return; }
        //if (Input.GetKeyDown(KeyCode.LeftShift) && NumBullet > 0f)
        //{
        //    isFiring = true;
        //    Invoke("Shoot", 0.2f);
        //}

        UpdateState();

        if (attackIntervalCountdown > 0)
        {
            attackIntervalCountdown -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        GameObject bulletObj = Instantiate(bullet, shootingPoint.position, Quaternion.identity);
        bulletObj.GetComponent<Bullet>().SetDirection(Mathf.Sign(transform.localScale.x));
        SoundManager.instance.PlaySound(shootSound);

        NumBullet--;
        attackIntervalCountdown = attackInterval;
    }

    void UpdateState()
    {
        if (isFiring)
        {
            animator.SetBool("isFiring", true);
            isFiring= false;
        }
        else
        {
            animator.SetBool("isFiring", false);
        }
    }

    public void AddBullet(float _value)
    {
        NumBullet = Mathf.Clamp(NumBullet + _value, 0f, maxBullet);
    }

    public void SubtractBullet()
    {
        NumBullet--;
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
}
