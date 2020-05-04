using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetCharacterStats : MonoBehaviour
{
    [SerializeField] private PlayerStatement player;
    [SerializeField] private Enemy[] enemies;

    void Start()
    {
        enemies = FindObjectsOfType<Enemy>();
        player = FindObjectOfType<PlayerStatement>();
    }
    
    public void SetStatsForAll()
    {
        player.WriteStats();
        foreach(var enemy in enemies)
            enemy.WriteStat();
    }
}
