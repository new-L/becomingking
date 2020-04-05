using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
class SendingBonusInfo : MonoBehaviour
{
    readonly string postURL = "https://becomingking.ru/dbmanager/bonusUpdate.php";

    public IEnumerator SendInfo()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", ServerData.GlobalUser);//добавление полей к форме отправления
        UnityWebRequest www = UnityWebRequest.Post(postURL, form);
        yield return www.SendWebRequest();//ждем
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error: " + www.error);
            yield break;
        }
        else
        {
           print(www.downloadHandler.text);
        }
        yield return new WaitForSeconds(0.1f);
    }

}