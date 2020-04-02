using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GetPlayerBuildings : MonoBehaviour
{

    //Ресурсы игрока
    readonly string postURLPlayer = "https://becomingking.ru/dbmanager/GetPlayerBuildings.php";

    public PlayerBuildings[] playerBuildings;
    [SerializeField] private SetBuildingData setBuilding;
    private void Start()
    {
        StartCoroutine(BuildingGetter());
    }
    public IEnumerator BuildingGetter()
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
            playerBuildings = JsonHelper.FromJson<PlayerBuildings>(json);
        }
        setBuilding.SetBuildingPlaceInfo();

    }
}


/*Класс для декода json файла и хранения данных о домах игрока*/
[Serializable]
public class PlayerBuildings
{
    public string buildingname;
    public int buildinglvl;
    public string place;
    public int building_id;
}
