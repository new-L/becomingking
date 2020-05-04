using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class GetDataBuildings : MonoBehaviour
{
    //Общие данные, необходимые для постройки зданий
    readonly string postURLPlayer = "https://becomingking.ru/dbmanager/GetDataBuildings.php";


    public static Building[] dataBuildings;
    [SerializeField] private GetPlayerBuildings buildings;

    public void Start()
    {
        StartCoroutine(GetBuildings());
    }

    public IEnumerator GetBuildings()
    {
        CoroutinesMaster.Add("DataBuildings", false);
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
            dataBuildings = JsonHelper.FromJson<Building>(json);
            CoroutinesMaster.EditValue("DataBuildings", true);
            StartCoroutine(buildings.BuildingGetter());
        }
    }

}

[Serializable]
public class Building
{
    //ID здания
    public int id;
    //Описание здания
    public string description;
    //Название здания
    public string name;
    //Стоимость постройки (в золоте)
    public int costgold;
    //Стоимость * на уровень для последующей построкйи
    public float multiplerank;
    //Начальные бонусы
    public int startbuff;
    //Насколько повысится бафф при повышении уровня
    public float uplvlbuff;
    //Что конкретно дает бафф %/предмет
    public string buffmeasurement;
    //Стоимость постройки (в дереве)
    public int costwood;
    //Стоимость постройки (в камне)
    public int costrock;
    //Стоимость постройки (в известняке)
    public int costlimestone;
    //Стоимость постройки (в пшенице)
    public int costwheat;
    //Код здания
    public string code;
}
