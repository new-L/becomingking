using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBarts : MonoBehaviour
{

    [SerializeField] private Image healthBar, energyBar;
    [SerializeField] private float 
        energyPrecent,
        healthPrecent;

    [SerializeField] private PlayerStatement player;
    private void Start()
    {
        player = FindObjectOfType<PlayerStatement>();
    }
    private void HealthBarPrecent()
    {
        healthPrecent = (float)player.CurrentHealth / player.MaxHealth;
    }

    private void EnergyBarPrecent()
    {
        energyPrecent = player.CurrentEnergy / player.MaxEnergy;
    }

    public void SetHealthBar()
    {
        HealthBarPrecent();
        if (healthPrecent > 0) healthBar.rectTransform.localScale = new Vector2(healthPrecent, healthBar.rectTransform.localScale.y);
        else healthBar.rectTransform.localScale = new Vector2(0, healthBar.rectTransform.localScale.y);
    }

    public void SetEnergyBar()
    {
        EnergyBarPrecent();
        if (energyPrecent > 0) energyBar.rectTransform.localScale = new Vector2(energyPrecent, energyBar.rectTransform.localScale.y);
        else energyBar.rectTransform.localScale = new Vector2(0, energyBar.rectTransform.localScale.y);
    }
}
