using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
public class GetItemsCharacter : MonoBehaviour
{
    readonly string postURL = "https://becomingking.ru/dbmanager/CharacterItem.php";

    public static CharacterItems items;
    public UnityEvent PotionSet;
    void Start()
    {
        StartCoroutine(GetItems());
    }
    public IEnumerator GetItems()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", ServerData.GlobalUser);
        UnityWebRequest www = UnityWebRequest.Post(postURL, form);
        yield return www.SendWebRequest();//ждем
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error: " + www.error);
            yield break;
        }
        else
        {
            items = JsonUtility.FromJson<CharacterItems>(www.downloadHandler.text);
            PotionSet.Invoke();
            yield break;
        }
    }
}

[Serializable]
public class CharacterItems
{
    public int healthpotion;
    public int energypotion;

}
