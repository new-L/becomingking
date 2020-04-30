using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private PlayerStatement player;
    private PlayerBarts bars;
    
    private void Start()
    {
        player = FindObjectOfType<PlayerStatement>();
        bars = FindObjectOfType<PlayerBarts>();
    }

    public void AddHealth(int health)
    {
        if (player.CurrentHealth + health <= player.MaxHealth) player.CurrentHealth += health;
        else player.CurrentHealth = player.MaxHealth;
        bars.SetHealthBar();
    }

    public void AddEnergy(float energy)
    {
        if (player.CurrentEnergy + energy <= player.MaxEnergy) player.CurrentEnergy += energy;
        else player.CurrentEnergy = player.MaxEnergy;
        bars.SetEnergyBar();
    }
}
