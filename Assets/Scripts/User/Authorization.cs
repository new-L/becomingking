using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Authorization : MonoBehaviour
{
    [SerializeField]
    private AlertAnimation alertAnimation;
    [SerializeField]
    private InputField nameField, passwordField;
    private string userLogin, userPass;

    readonly string postURL = "https://becomingking.ru/dbmanager/authorizeCheck.php";


    public void AuthUser()
    {
        userLogin = nameField.text;
        userPass = passwordField.text;

        if(string.IsNullOrEmpty(userLogin) || string.IsNullOrEmpty(userPass))
        {
            alertAnimation.AnimationActivate("Вы ввели не все данные!");
        }
        else
        {
            StartCoroutine(Send());
        }
    }

    private IEnumerator Send()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", userLogin);//добавление полей к форме отправления
        form.AddField("pass", userPass);//добавление полей к форме отправления
        UnityWebRequest www = UnityWebRequest.Post(postURL, form);
        yield return www.SendWebRequest();//ждем
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error: " + www.error);
            yield break;
        }
        else
        {  
            Debug.Log("Server_AUTH: " + www.downloadHandler.text);
            if (www.downloadHandler.text.Equals("1")) { ServerData.GlobalUser = userLogin; SceneManager.LoadScene("GameTasks"); }
            else alertAnimation.AnimationActivate("Пользователя с такими данными не существует!");
            yield break;
        }

    }


}
