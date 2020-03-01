using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ServerController : MonoBehaviour
{
    readonly string postURL = "https://becomingking.ru/dbmanager/updateTask.php";
    public SceneJump sceneJump;
    public IEnumerator SaveDateWorker()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", ServerData.GlobalUser);
        form.AddField("task_type", TaskData.TypeTask);//добавление полей к форме отправления
        form.AddField("resource_count", TaskData.ResourceReward);//добавление полей к форме отправления
        form.AddField("exp", TaskData.Exptotask);//добавление полей к форме отправления
        form.AddField("rating", TaskData.Rating);//добавление полей к форме отправления
        form.AddField("diamond", TaskData.DiamondCount);//добавление полей к форме отправления
        if (TaskData.TypeTask.Equals("army")) form.AddField("army_losses", TaskData.ArmySLosses);
        else if (TaskData.TypeTask.Equals("worker")) form.AddField("resource_type", TaskData.TypeResource);//добавление полей к форме отправления
        UnityWebRequest www = UnityWebRequest.Post(postURL, form);
        yield return www.SendWebRequest();//ждем
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error: " + www.error);
            yield break;
        }
        else
        {
            if(www.downloadHandler.text.Equals("All right"))
            {
                TaskData.IsComplete = true;
                sceneJump.JumpToScene("GameTasks");
            }
        }
    }
}
