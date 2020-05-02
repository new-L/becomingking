using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DyingTrigger : MonoBehaviour
{
    private PlayerStatement player; 
    private PlayerMovement spawn;
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


    public IEnumerator OnEnter()
    {
        yield return new WaitForSeconds(2f);
        HuntLevelData.CoinChessCount = 0;
        HuntLevelData.CoinCount = 0;
        HuntLevelData.ResourceCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
