using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DBManager : MonoBehaviour
{
    readonly string getURL = "https://becomingking.ru/dbmanager/connection.php";
    readonly string postURL = "https://becomingking.ru/dbmanager/connection.php";


    private void Start()
    {
        ServerData.ServerConnection = false;
    }

    public void Response(bool mode)
    {
        if(!mode) StartCoroutine(Send());
    }


    private IEnumerator Send()
    {
        WWWForm form = new WWWForm();
        form.AddField("Connection","ClientConnected");//добавление полей к форме отправления
        UnityWebRequest www = UnityWebRequest.Post(postURL, form);
        yield return www.SendWebRequest();//ждем
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error: " + www.error);
            ServerData.ServerConnection = false;
            yield break;
        }
        else
        {
            ServerData.ServerConnection = true;
            Debug.Log("Server: " + www.downloadHandler.text);
        }

    }

}
