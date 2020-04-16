using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Build : MonoBehaviour
{
    private readonly string postURL = "https://becomingking.ru/dbmanager/updateCharacterBuildings.php";
    [SerializeField] private PlayerServerResponse serverResponse;
    [SerializeField] private GetResourcePlayer resourcePlayer;
    [SerializeField] private GetPlayerBuildings getPlayerBuildings;
    [SerializeField] private PopUpMenus popMenus;
    [SerializeField] private GameObject menu;

    public IEnumerator SendResponse(string option, Button buildB)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", ServerData.GlobalUser);//добавление полей к форме отправления
        form.AddField("place", TownData.ChosenPlace);//добавление полей к форме отправления
        form.AddField("code", TownData.BuildingCode);//добавление полей к форме отправления
        form.AddField("option", option);//добавление полей к форме отправления
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
        yield return new WaitForSeconds(0.03f);
        StartCoroutine(getPlayerBuildings.BuildingGetter());
        yield return new WaitForSeconds(0.03f);
        StartCoroutine(serverResponse.Send());
        yield return new WaitForSeconds(0.03f);
        StartCoroutine(resourcePlayer.ResourceGetter());
        yield return new WaitForSeconds(0.03f);
        if(menu.activeSelf) popMenus.Close(menu);
        buildB.enabled = true;
    }
}
