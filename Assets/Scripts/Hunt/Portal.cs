using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Character"))
        {
            if (gameObject.tag.Equals("PortalIn")) PlayerMovement.isPortalIn = true;
            else if (gameObject.tag.Equals("PortalOut")) PlayerMovement.isPortalOut = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Character"))
        {
            if (gameObject.tag.Equals("PortalIn")) PlayerMovement.isPortalIn = false;
            else if (gameObject.tag.Equals("PortalOut")) PlayerMovement.isPortalOut = false;
        }
    }
}
