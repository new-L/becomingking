using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oclussion : MonoBehaviour
{
    public GameObject character;
    [SerializeField] private GameObject[] enemiesPrefab;
    [SerializeField] private List<GameObject> enemies;
    private void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character");
        enemiesPrefab = GameObject.FindGameObjectsWithTag("Enemy");
        enemies.AddRange(enemiesPrefab);
        InvokeRepeating("LookForPlayer", 0, 0.2f);
    }
    private void LookForPlayer()
    {
        foreach (var item in enemies)
        {
            float distance = Vector2.Distance(character.transform.position, item.transform.position);
            if (distance < 25f)
            {
                item.SetActive(true);
            }
            else if (distance > 25f)
            {
                item.SetActive(false);
            }
        }
    }


    public void DeleteEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

}
