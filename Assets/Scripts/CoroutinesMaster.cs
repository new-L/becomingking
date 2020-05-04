using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutinesMaster : MonoBehaviour
{
    public static Dictionary<string, bool> coroutinesLoader = new Dictionary<string, bool>();
    [SerializeField] private GameObject LoadPanel;
    void Start()
    {
        LoadPanel.SetActive(true);
        InvokeRepeating("Timer", 1f, 1f);
    }

    public static void Add(string name, bool state)
    {
        bool flag = true;
        foreach (var item in coroutinesLoader) {
            if (item.Equals(name)) flag = false;
        }
        if(flag) coroutinesLoader.Add(name, state);
    }
    private int m_Timer = 2;
    public static void EditValue(string name, bool newState)
    {
        coroutinesLoader[name] = newState;
    }
    private void Timer()
    {
        m_Timer--;
        if (m_Timer == 0 && CheckCoroutines())
        {
            LoadPanel.SetActive(false);
            CancelInvoke();
            coroutinesLoader.Clear();
        }
        else if (m_Timer == 0 && !CheckCoroutines()) 
        {
            m_Timer = 3;
        }
    }
    private static bool CheckCoroutines()
    {
        bool flag = true;
        foreach (var item in coroutinesLoader)
        {
            if (!item.Value) flag = false;
        }
        return flag;
    }


}
