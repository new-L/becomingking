using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsTrigger : MonoBehaviour
{
    private PlayerStatement player;
    private bool inLava;
    private void Start()
    {
        player = FindObjectOfType<PlayerStatement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Character"))
        {
            if (gameObject.tag.Equals("Lava")) player.TakeDamage(player.MaxHealth / 100 * 15);
            else if(gameObject.tag.Equals("Trap")) player.Dying();
        }
        
    }

            
}
