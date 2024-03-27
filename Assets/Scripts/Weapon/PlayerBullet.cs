using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerBullet : Bullet
{
    
    //void Awake()
    //{
    //    animator = GetComponent<Animator>();

    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage();

            SoundManager.instance.PlaySound(explosionAudio);
            animator.SetTrigger("explode");

        }
        else animator.SetTrigger("shootHit");

    }
}
