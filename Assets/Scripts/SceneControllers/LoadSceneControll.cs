using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneControll : MonoBehaviour
{
   // [SerializeField]
    //private PreLoadLoader preLoadLoader;
    private void Start()
    {
      //  preLoadLoader = GameObject.Find("loader").GetComponent<PreLoadLoader>();
    }
    private void Update()
    {
        if (ServerData.ServerConnection) SceneManager.LoadScene("Аuthorization");
       // else preLoadLoader.AnimationActive();
    }

}
