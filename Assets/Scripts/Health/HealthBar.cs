using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
     public Player playerHealth;
    [SerializeField] private Image currenthealthBar;

    private void Start()
    {
        currenthealthBar.fillAmount = playerHealth.hitpoints.value / 10;
    }

    private void Update()
    {
        currenthealthBar.fillAmount = playerHealth.hitpoints.value / 10;
    }
}
