using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oclussion : MonoBehaviour
{
    private void Start()
    {
        //gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        print(other.tag);
        if (other.gameObject.tag.Equals("PlayerCam"))
        {
            gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        print(other.tag);
        if (other.tag == "PlayerCam")
        {
            gameObject.SetActive(false);
        }
    }
}
