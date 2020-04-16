using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
class SendingBonusInfo : MonoBehaviour
{
    readonly string postURL = "https://becomingking.ru/dbmanager/bonusUpdate.php";
    readonly string offlineURL = "https://becomingking.ru/dbmanager/OfflineBonus.php";
    [SerializeField] private SetBonusInfo bonusInfo;
    public static OfflineBonus[] bonuses;
    public IEnumerator SendInfo(bool IsOffline)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", ServerData.GlobalUser);//добавление полей к форме отправления
        form.AddField("offline", IsOffline.ToString());//добавление полей к форме отправления
        if (IsOffline)
        {
            form.AddField("minutes", TimerData.TotalMinutesOffline);//добавление полей к форме отправления
        }
        UnityWebRequest www = UnityWebRequest.Post(postURL, form);
        yield return www.SendWebRequest();//ждем
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error: " + www.error);
            yield break;
        }
        else
        {
            if (IsOffline && !String.IsNullOrEmpty(www.downloadHandler.text))
            {
                string json = JsonHelper.fixJson(www.downloadHandler.text);
                bonuses = JsonHelper.FromJson<OfflineBonus>(json);
                bonusInfo.SetBonus();
            }
        }
        yield return new WaitForSeconds(0.3f);
    }

    public IEnumerator SendOfflineCheck(string offlineCheckType)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", ServerData.GlobalUser);//добавление полей к форме отправления
        form.AddField("type", offlineCheckType);//добавление полей к форме отправления
        if (offlineCheckType.Equals("post"))
        {
            form.AddField("starttime", DateTime.Now.ToString());//добавление полей к форме отправления
        }
        UnityWebRequest www = UnityWebRequest.Post(offlineURL, form);
        yield return www.SendWebRequest();//ждем
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error: " + www.error);
            yield break;
        }
        else
        {
            if(offlineCheckType.Equals("get") && !www.downloadHandler.text.Equals("none"))
            {
                TimerData.DateTime = DateTime.Parse(www.downloadHandler.text);
                bonusInfo.OfflineChecker();
                StartCoroutine(SendInfo(true));
            }
        }
        yield return new WaitForSeconds(0.3f);
    }

}

/*Класс для декода json файла и хранения данных о бонусах игрока, пока он был оффлайн*/
[Serializable]
public class OfflineBonus
{
    public string type;
    public int bonus;
}

