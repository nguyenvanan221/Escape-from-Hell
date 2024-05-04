using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCollectible : MonoBehaviour
{
    [SerializeField] private float bulletValue;
    [SerializeField] private AudioClip pickUpGunAudio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Gun>().AddBullet(bulletValue);
            SoundManager.Instance.PlaySound(pickUpGunAudio);
            gameObject.SetActive(false);
        }
    }
}
