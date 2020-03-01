using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class NewUser : MonoBehaviour
{
    [SerializeField]
    private AlertAnimation alertAnimation;
    readonly string getURL = "https://becomingking.ru/dbmanager/logincheck.php";
    readonly string postURL = "https://becomingking.ru/dbmanager/registr.php";
    [SerializeField]
    private string userLogin, userPass, userMail;
    [SerializeField]
    private InputField loginIF, passIF, mailIF;
    private void Start()
    {
        alertAnimation = GameObject.Find("Alert").GetComponent<AlertAnimation>();
    }


    //Нужно для отправку по нажатию на кнопку
    public void RegistrUser()
    {
        userLogin = loginIF.text;
        userPass = passIF.text;
        userMail = mailIF.text;
        
        if (!userLogin.Equals("") && !userPass.Equals("") && !userMail.Equals(""))
            {
                if (PassValidate() && MailValidate()) StartCoroutine(GetUser());
            }
            else
            {
                alertAnimation.AnimationActivate("Вы ввели не все данные!");
            }
    }

    //Валидация пароля
    private bool PassValidate()
    {
        var hasLat = new Regex(@"[a-z]+");
        var hasUpperLat = new Regex(@"[A-Z]+");
        var hasMinimum = new Regex(@".{8,}");
        var hasCirillica = new Regex(@"[а-я]+");
        var hasUpperCirillica = new Regex(@"[А-Я]+");
        var isValidated = hasCirillica.IsMatch(userPass) || hasUpperCirillica.IsMatch(userPass);
        if (!isValidated)
        {
            isValidated = (hasLat.IsMatch(userPass) || hasUpperLat.IsMatch(userPass)) && hasMinimum.IsMatch(userPass);
            if (!isValidated)
            {
                alertAnimation.AnimationActivate("Пароль должен содержать только символы английского алфавита, длинной не меньше 8");
                return false;
            }
            else return true;
        }
        else
        {
            alertAnimation.AnimationActivate("Извините, но пароль содержит русские символы. Это недопустимо");
            return false;
        }
    }


    //Валидация почты
    private bool MailValidate()
    {
        var hasMail = new Regex(@"^(|(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6})$");
        var isValidated = hasMail.IsMatch(userMail);
        return isValidated; 
    }


    private IEnumerator UserRegistration()
    {
        
        WWWForm form = new WWWForm();
        form.AddField("name", userLogin);//добавление полей к форме отправления
        form.AddField("pass", userPass);//добавление полей к форме отправления
        form.AddField("mail", userMail);//добавление полей к форме отправления
        UnityWebRequest www = UnityWebRequest.Post(postURL, form);
        yield return www.SendWebRequest();//ждем
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error: " + www.error);
            yield break;
        }
        else
        {
            Debug.Log("Сервер ответил: " + www.downloadHandler.text);
            ServerData.GlobalUser = userLogin;
            SceneManager.LoadScene("GameTasks");
        }
    }

    //Проверка на уникальность логина
    private IEnumerator GetUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", userLogin);
        UnityWebRequest www = UnityWebRequest.Post(getURL, form);
        yield return www.SendWebRequest();//ждем
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error: " + www.error);
            yield break;
        }
        else
        {
            Debug.Log("Сервер ответил: " + www.downloadHandler.text);
            if(www.downloadHandler.text == "" || www.downloadHandler.text.Equals("") || www.downloadHandler.text == "Пользователь не найден!" || www.downloadHandler.text.Equals("Пользователь не найден!"))
            {
                yield return StartCoroutine(UserRegistration());
                yield break;
            }
            else
            {
                alertAnimation.AnimationActivate("Логин существует!");
                yield break;
            }
        }
        
    }

}
