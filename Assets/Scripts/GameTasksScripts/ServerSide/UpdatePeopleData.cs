using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;


//Это нужно чисто для обновления данных ресурсов. Хотя данные мы уже считываем в другому скрипте, было решно не использовать его повторно, т.к. тот скрипт будет использоваться для считывания большого количества данных
public class UpdatePeopleData : MonoBehaviour
{
    readonly string postURL = "https://becomingking.ru/dbmanager/playerInfo.php";
    private PeopleData inData;
    public Population population;
    public Army army;
    public Workers worker;
    public void UpdatePeople()
    {
        inData = new PeopleData();
        StartCoroutine(SendResponse());
    }
    public IEnumerator SendResponse()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", ServerData.GlobalUser);//добавление полей к форме отправления
        form.AddField("type", "people");//добавление полей к форме отправления
        UnityWebRequest www = UnityWebRequest.Post(postURL, form);
        yield return www.SendWebRequest();//ждем
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error: " + www.error);
            yield break;
        }
        else
        {
            inData = JsonUtility.FromJson<PeopleData>(www.downloadHandler.text);
            PeopleDataSave();
        }
        yield return new WaitForSeconds(0.1f);
    }

    public UnityEvent CheckTheAccess;

    private void PeopleDataSave()
    {
        population.Count = inData.social; 
        army.Count = inData.army; 
        worker.Count = inData.worker;
        CheckTheAccess.Invoke();
    }

}

[Serializable]
public class PeopleData
{
    /**/
    public int social;
    public int worker;
    public int army;
    /**/
}
