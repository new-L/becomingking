using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AuthorizationBtnController : MonoBehaviour
{

    public void RegBtn()
    {
        SceneManager.LoadScene("Registration");
    }

}
