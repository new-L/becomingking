using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerData : MonoBehaviour
{
    //Статус коннекта к серверу
    private static bool serverConnection;
    public static bool ServerConnection { get => serverConnection; set => serverConnection = value; }


    //Поле с логином юзеры, который играет на данный момент
    private static string globalUser;
    public static string GlobalUser { get => globalUser; set => globalUser = value; }
}
