using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private AudioClip pickUpEnergyaudio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UIManager.Instance.AddEnergy(1);
            SoundManager.Instance.PlaySound(pickUpEnergyaudio);
            gameObject.SetActive(false);
        }
    }
}
