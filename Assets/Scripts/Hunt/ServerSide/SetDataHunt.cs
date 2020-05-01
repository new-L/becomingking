using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetDataHunt : MonoBehaviour
{
    readonly string postURL = "https://becomingking.ru/dbmanager/HuntDataUpdate.php";

    [SerializeField] private WinPanel winPanel;

    public void SendResponse()
    {
        StartCoroutine(SendDataResponse());
    }

    private IEnumerator SendDataResponse()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", "newL");//ServerData.GlobalUser);
        form.AddField("coin_count", HuntLevelData.CoinCount);//добавление полей к форме отправления
        form.AddField("chess_count", HuntLevelData.CoinChessCount);//добавление полей к форме отправления
        form.AddField("resource_count", HuntLevelData.ResourceCount);//добавление полей к форме отправления
        UnityWebRequest www = UnityWebRequest.Post(postURL, form);
        yield return www.SendWebRequest();//ждем
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error: " + www.error);
            yield break;
        }
        else
        {
            if(www.downloadHandler.text.Equals("All right"))
            {
                winPanel.m_Exit.SetActive(true);
            }
        }
    }
}
