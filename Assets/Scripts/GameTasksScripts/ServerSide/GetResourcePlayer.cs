using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetResourcePlayer : MonoBehaviour
{
    //Ресурсы игрока
    readonly string postURLPlayer = "https://becomingking.ru/dbmanager/GetPlayerResource.php";

    public PlayerResources[] playerResources;
    [SerializeField] private SetResourceInfo setResource;
    private void Start()
    {
        GetResource();
    }
    public void GetResource()
    {
        StartCoroutine(ResourceGetter());
    }
    public IEnumerator ResourceGetter()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", ServerData.GlobalUser);//добавление полей к форме отправления
        UnityWebRequest www = UnityWebRequest.Post(postURLPlayer, form);
        yield return www.SendWebRequest();//ждем
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error: " + www.error);
            yield break;
        }
        else
        {
            string json = JsonHelper.fixJson(www.downloadHandler.text);
            playerResources = JsonHelper.FromJson<PlayerResources>(json);
        }
        setResource.SetResources();

    }

}
/*Класс для декода json файла и хранения данных о ресурсах игрока*/
[Serializable]
public class PlayerResources
{
    public string name;
    public int count;
    public string code;
}

