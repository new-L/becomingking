using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class GameServerResponse : MonoBehaviour
{
    readonly string postURL = "https://becomingking.ru/dbmanager/gameInfo.php";

    [SerializeField]
    private List<string> dataType = new List<string>() { "leveling" };

    //Данные о уровне будут содержаться тут
    [SerializeField]
    private static GameData[] gameData;
    void Start()
    {
        foreach (string indexer in dataType)
        {
            StartCoroutine(Send(indexer));
        }
    }


    //Общение с сервером(берем данные лвлинга)
    public IEnumerator Send(string type)
    {
        CoroutinesMaster.Add(type, false);
        WWWForm form = new WWWForm();
        form.AddField("type", type);//добавление полей к форме отправления
        UnityWebRequest www = UnityWebRequest.Post(postURL, form);
        yield return www.SendWebRequest();//ждем
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error: " + www.error);
            yield break;
        }
        else
        {
            string json = JsonHelper.fixJson(www.downloadHandler.text);
            gameData = JsonHelper.FromJson<GameData>(json);
            CoroutinesMaster.EditValue(type, true);
            yield break;
        }

    }
    

}









[Serializable]
 public class GameData
{
    public int level;
    public int exp_min;
    public int exp_up;
}

