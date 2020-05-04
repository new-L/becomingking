using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerServerResponse : MonoBehaviour
{
    readonly string postURL = "https://becomingking.ru/dbmanager/playerInfo.php";
    [SerializeField] private SetAccountDataInfo controller;
    [SerializeField] private List<string> dataType = new List<string>() { "currency" , "info" , "people" };
    private InData inData;
    /*Объекты со скриптами для записи данных*/
    [SerializeField] private Diamond diamond;
    [SerializeField] private Gold gold;
    [SerializeField] private CharacterLevel cLevel;
    public Population population;
    public Army army;
    public Workers worker;
    /***************************************/
    void Start()
    {
        inData = new InData();
        population = GameObject.FindGameObjectWithTag("HCharacter").GetComponent<Population>();
        army = GameObject.FindGameObjectWithTag("HCharacter").GetComponent<Army>();
        worker = GameObject.FindGameObjectWithTag("HCharacter").GetComponent<Workers>();
        StartCoroutine(Send());

    }
    public IEnumerator Send()
    {
        CoroutinesMaster.Add("AllData", false);
        foreach (string indexer in dataType)
        {
            WWWForm form = new WWWForm();
            form.AddField("name", ServerData.GlobalUser);//добавление полей к форме отправления
            form.AddField("type", indexer);//добавление полей к форме отправления
            UnityWebRequest www = UnityWebRequest.Post(postURL, form);
            yield return www.SendWebRequest();//ждем
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Error: " + www.error);
                yield break;
            }
            else
            {
                inData = JsonUtility.FromJson<InData>(www.downloadHandler.text);
                CoroutinesMaster.EditValue("AllData", true);
                DataSave(indexer);
            }
        }
    }



    private void DataSave(string typeData)
    {
        switch (typeData)
        {
            case "currency": { gold.Count = inData.gold; diamond.Count = inData.diamond; break; }
            case "info": { cLevel.Level = inData.level; cLevel.Exp = inData.xp; break; }
            case "people": { population.Count = inData.social; army.Count = inData.army; worker.Count = inData.worker;  break; }
        }
        controller.SetData();
    }


}
//Записываем сначала сюда, потому что напрямую не получится(сериализация там не нужна)
[Serializable]
public class InData
{
    /**/
    public int gold;
    public int diamond;
    /**/
    public int level;
    public int xp;
    /**/
    public int social;
    public int worker;
    public int army;
    /**/
}

