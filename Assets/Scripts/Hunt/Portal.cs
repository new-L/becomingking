using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Alert alert;
    private PlayerItemsCount playerUI;
    private void Start()
    {
        alert = FindObjectOfType<Alert>();
        playerUI = FindObjectOfType<PlayerItemsCount>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Character"))
        {
            if (gameObject.tag.Equals("PortalIn")) PlayerMovement.isPortalIn = true;
            else if (gameObject.tag.Equals("PortalOut")) PlayerMovement.isPortalOut = true;
            playerUI.playerAlert.text = "Нажмите 'F', для телепорта";
            alert.AlertOn();
        }
        

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Character"))
        {
            if (gameObject.tag.Equals("PortalIn")) PlayerMovement.isPortalIn = false;
            else if (gameObject.tag.Equals("PortalOut")) PlayerMovement.isPortalOut = false;
            alert.AlertOff();
        }
        
    }
}
