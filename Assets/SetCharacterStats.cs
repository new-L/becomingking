using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCharacterStats : MonoBehaviour
{
    [SerializeField] private PlayerAtack player;
    [SerializeField] private Enemy[] enemies;
    void Start()
    {
        enemies = FindObjectsOfType<Enemy>();
        player = FindObjectOfType<PlayerAtack>();
    }
    
    public void SetStatsForAll()
    {
        player.WriteStats();
        foreach(var enemy in enemies)
            enemy.WriteStat();
    }
}
