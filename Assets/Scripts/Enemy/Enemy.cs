using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeEnemy
{
    Move, Idle
}
public class Enemy : MonoBehaviour
{
    [SerializeField] private float startingHitPoints;
    [SerializeField] private TypeEnemy typeEnemy;
    public float HitPoints { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage();
        }
    }

    private void Awake()
    {
        HitPoints = startingHitPoints;
        EnemyMove enemy = GetComponent<EnemyMove>();
        if (enemy != null && typeEnemy == TypeEnemy.Move)
        {
            enemy.enabled = true;
        }
        else if (enemy != null)
        {
            enemy.enabled = false;
        }
    }

    public void TakeDamage()
    {
        HitPoints--;

        if (HitPoints <= float.Epsilon)
        {
            gameObject.SetActive(false);
        }
    }
}
