using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GetPlayerBuildings : MonoBehaviour
{

    //Ресурсы игрока
    readonly string postURLPlayer = "https://becomingking.ru/dbmanager/GetPlayerBuildings.php";

    public PlayerBuildings[] playerBuildings;
    [SerializeField] private SetBuildingData setBuilding;
    [SerializeField] private SetTimerInfo timerInfo;
    private void Start()
    {
        //StartCoroutine(BuildingGetter());
    }
    public IEnumerator BuildingGetter()
    {
        CoroutinesMaster.Add("PlayerBuildings", false);
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
            CoroutinesMaster.EditValue("PlayerBuildings", true);
        }
        if(SceneManager.GetActiveScene().name.Equals("Town")) setBuilding.SetBuildingPlaceInfo();
        timerInfo.GetBuildingsBonus();
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
