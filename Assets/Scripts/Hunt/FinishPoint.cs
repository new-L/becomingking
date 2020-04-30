using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public static List<bool> goalCheck = new List<bool>();
    public static bool goldKey, silverKey, allMonsters;

    [SerializeField] private GameObject m_Borders;

    
    private void Start()
    {
        m_Borders.SetActive(true);
        goalCheck.Clear();
    }

    public void Finish()
    {
        print(goalCheck.Count);
        bool flag = true;
        if (goalCheck.Count == 2)
        {
            foreach (var item in goalCheck)
            {
                print(item);
                if (!item) flag = false;
            }
            if (flag) m_Borders.SetActive(false);
        }
        
    }
}
