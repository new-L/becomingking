using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SocialTaskHandler : MonoBehaviour
{
    public PlayerServerResponse playerSR;
    public GameTasksController gmController;
    readonly string postURL = "https://becomingking.ru/dbmanager/updateTask.php";
    public void TaskTypeDefin(string def)
    {
        StartCoroutine(SaveDateWorker(def));
    }

    public IEnumerator SaveDateWorker(string def)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", ServerData.GlobalUser);
        form.AddField("task_type", TaskData.TypeTask);//добавление полей к форме отправления
        form.AddField("people_type", def);//добавление полей к форме отправления
        switch (def)
        {
            case "social":
                {
                    form.AddField("resource_count", TaskData.SocialPReward);//добавление полей к форме отправления
                    break;
                }
            case "worker":
                {
                    form.AddField("resource_count", TaskData.WorkerPReward);//добавление полей к форме отправления
                    form.AddField("losses", TaskData.GoldWLosses);
                    break;
                }
            case "army":
                {
                    form.AddField("resource_count", TaskData.ArmyPReward);//добавление полей к форме отправления
                    form.AddField("losses", TaskData.GoldALosses);
                    break;
                }
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
            print(www.downloadHandler.text);
            if (www.downloadHandler.text.Equals("All right"))
            {
                StartCoroutine(playerSR.Send());
                TaskData.IsComplete = true;
                gmController.TaskComplete();
            }
                   
        }
    }

}
