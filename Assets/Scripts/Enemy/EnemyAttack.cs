using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootingPoint;

    [SerializeField] private float attackCooldown;
    private Animator animator;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        animator= GetComponent<Animator>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
        {
            cooldownTimer= 0;
            animator.SetTrigger("attack");
            Invoke("Shoot", 0.2f);
        }
    }

    void Shoot()
    {
        GameObject bulletObj = Instantiate(bullet, shootingPoint.position, Quaternion.identity);
        bulletObj.GetComponent<Bullet>().SetDirection(Mathf.Sign(transform.localScale.x) * -1);
    }

}
