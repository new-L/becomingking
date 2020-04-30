using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DyingTrigger : MonoBehaviour
{
    private PlayerStatement player; 
    private PlayerMovement spawn;
    private GameObject startPoint;
    private void Start()
    {
        player = FindObjectOfType<PlayerStatement>();
        spawn  = FindObjectOfType<PlayerMovement>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Character"))
        {
            StartCoroutine(OnEnter());
        }
    }


    IEnumerator OnEnter()
    {
        yield return new WaitForSeconds(.4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
