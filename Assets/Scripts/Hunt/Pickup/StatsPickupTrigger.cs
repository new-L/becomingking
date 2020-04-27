using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPickupTrigger : MonoBehaviour
{
    private PlayerStats m_Stats;
    private PlayerStatement m_Player;
    private ItemRespawn m_Respawns;

    public bool isPickupItem = false;

    public int precent;
    private void Start()
    {
        m_Stats = FindObjectOfType<PlayerStats>();
        m_Player = FindObjectOfType<PlayerStatement>();
        m_Respawns = FindObjectOfType<ItemRespawn>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Character"))
            isPickupItem = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Character"))
            isPickupItem = false;
    }
    private void Update()
    {
        if(isPickupItem && Input.GetKeyDown(KeyCode.E))
        {
            switch (gameObject.tag) 
            {
                case "HealthPickup":
                    {
                        if (m_Player.CurrentHealth < m_Player.MaxHealth)
                        {
                            m_Stats.AddHealth(m_Player.MaxHealth / 100 * precent);
                            m_Respawns.InActiveItem(gameObject.transform.parent.gameObject);
                        }
                        break;
                    }
                case "EnergyPickup":
                    {
                        if (m_Player.CurrentEnergy < m_Player.MaxEnergy)
                        {
                            m_Stats.AddEnergy((m_Player.MaxEnergy / 100) * (precent/2));
                            m_Respawns.InActiveItem(gameObject.transform.parent.gameObject);
                        }
                        break;
                    }
            }
        }

        
    }
}
