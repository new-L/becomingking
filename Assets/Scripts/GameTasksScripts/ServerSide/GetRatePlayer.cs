using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GetRatePlayer : MonoBehaviour
{
    //Личный рейтинг игрока
    readonly string postURLPlayer = "https://becomingking.ru/dbmanager/GetPlayerRate.php";
    //Мировые рейтинги
    readonly string postURLPlayers = "https://becomingking.ru/dbmanager/playerInfo.php";
    [SerializeField]
    private SetRateInfo setRateInfo;
    [SerializeField]
    private Rating rating;
    
    public static WorldRating[] worldRating;
    public void UpdateItems()
    { 
        StartCoroutine(WorldRate());
    }

  //Корутина для поиска мирового рейтинга
    public IEnumerator WorldRate()
    {
            WWWForm form = new WWWForm();
            form.AddField("name", ServerData.GlobalUser);//добавление полей к форме отправления
            form.AddField("type", "rating");//добавление полей к форме отправления
            UnityWebRequest www = UnityWebRequest.Post(postURLPlayers, form);
            yield return www.SendWebRequest();//ждем
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Error: " + www.error);
                yield break;
            }
            else
            {
                string json = JsonHelper.fixJson(www.downloadHandler.text);
                worldRating = JsonHelper.FromJson<WorldRating>(json);
                print(json);
            }
        setRateInfo.SetWorldRate();
        StartCoroutine(Send());
    }

    //Корутина для поиска личного рейтинга
    public IEnumerator Send()
    {
        string serverText;
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
            print(www.downloadHandler.text);
            serverText = www.downloadHandler.text;
        }
        PlayerRatingWrite(serverText);
    }


    //Запись данных рейтинга пользователя в другой класс
    private void PlayerRatingWrite(string serverResponse)
    {
        //Сам рейтинг игрока
        Regex regex = new Regex(@"^[0-9]*");
        MatchCollection matches = regex.Matches(serverResponse);
        foreach (Match match in matches)
        {
            rating.PlayerRating = Convert.ToInt64(match.Value);
        }

        //Его место в мировом рейтинге
        regex = new Regex(@"(?!\|).[0-9]*$");
        matches = regex.Matches(serverResponse);
        foreach (Match match in matches)
        {
            rating.PlayerSerialNumber = Convert.ToInt32(match.Value);
        }
        setRateInfo.SetRate();
        StopAllCoroutines();
    }


}
/*Класс для декода json файла и хранения данных о мировом рейтинге*/
[Serializable]
public class WorldRating
{
    public string name;
    public int numberrate;
    public int level;
}

