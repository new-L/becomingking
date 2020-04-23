using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using System;
public class GetPlayerStats : MonoBehaviour
{
    readonly string postURL = "https://becomingking.ru/dbmanager/playerInfo.php";

    public static PlayerStatsInfo playerStats;
    public PlayerAtack player;

    public UnityEvent AllDataREcieved;
    public void Start()
    {
        StartCoroutine(GetResponse());
    }
    public IEnumerator GetResponse()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", "newL");//ServerData.GlobalUser);
        form.AddField("type", "info");//добавление полей к форме отправления
        UnityWebRequest www = UnityWebRequest.Post(postURL, form);
        yield return www.SendWebRequest();//ждем
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error: " + www.error);
            yield break;
        }
        else
        {
            playerStats = JsonUtility.FromJson<PlayerStatsInfo>(www.downloadHandler.text);
            AllDataREcieved.Invoke();
            yield break;
        }
    }
}
[Serializable]
public class PlayerStatsInfo
{
    public int level;
    public int damage;
    public int armor;    
    public int health;
    
}
