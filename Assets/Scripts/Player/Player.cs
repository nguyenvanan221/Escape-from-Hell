using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float startingHitPoints;
    public HitPoints hitpoints;

    [SerializeField] private AudioClip sound;
    private Animator animator;

    private void Awake()
    {
        hitpoints.value = startingHitPoints;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage()
    {
        hitpoints.value--;
        animator.SetTrigger("isHurt");
        SoundManager.instance.PlaySound(sound);
    }

    public void Respawn()
    {
        animator.ResetTrigger("isHurt");
        animator.Play("Move");
    }

    public void Reset()
    {
        hitpoints.value = startingHitPoints;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lava"))
        {
            TakeDamage();
        }
    }
}
