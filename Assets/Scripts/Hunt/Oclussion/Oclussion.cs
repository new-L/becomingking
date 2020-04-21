using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oclussion : MonoBehaviour
{
    public GameObject character;
    public GameObject enemy;
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character");
        InvokeRepeating("LookForPlayer", 0, 0.1f);
    }
    void LookForPlayer()
    {
        float distance = Vector2.Distance(character.transform.position, enemy.transform.position);
        enemy.SetActive(distance < 25f);
    }

}
