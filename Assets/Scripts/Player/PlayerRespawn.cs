using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform currentCheckpoint;
    private Player playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Player>();
 
    }

    public void RespawnCheck()
    {
        if (playerHealth.hitpoints.value <= 0f)
        {
            UIManager.Instance.GameOver();
            return;
        }

        playerHealth.Respawn();
        transform.position = currentCheckpoint.position;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            
            currentCheckpoint = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;
        }
    }
}
