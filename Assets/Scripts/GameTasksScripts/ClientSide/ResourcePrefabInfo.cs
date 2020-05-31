using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcePrefabInfo : MonoBehaviour
{
    [SerializeField] private GameObject resourceInfoPanel;
    [SerializeField] private Text m_ResName, m_ResCount;
    [SerializeField] private Image m_ResIcon;
    [SerializeField] private RectTransform m_Arrow;
    /*Ресуры игрока*/
    [SerializeField] private GetResourcePlayer getResource;
    private bool m_IsSubmited;
    private string chosenRes;
    //При наведении на ресурс в панели
    public void OnResourceFocus(Text code)
    {
        if (!m_IsSubmited)
        {
            m_Arrow.localRotation = Quaternion.Euler(0, 180, 0);
            SetResourceInfo(code);
        }    
    }

    //При выборе ресурса
    public void Toggle(Text code)
    {
        if (!m_IsSubmited)
        {
            m_Arrow.localRotation = Quaternion.Euler(0, 0, 0);
            m_IsSubmited = true;
            chosenRes = code.text;
        }
        else
        {
            if (chosenRes.Equals(code.text))
            {
                m_Arrow.localRotation = Quaternion.Euler(0, 180, 0);
                m_IsSubmited = false;
            }
            else
            {
                chosenRes = code.text;
                SetResourceInfo(code);
            }
        }
    }

    //При НЕ наведении на ресурс в панели
    public void ResourceUnFocus()
    {
        if(!m_IsSubmited) resourceInfoPanel.SetActive(false);
    }


    private void SetResourceInfo(Text code)
    {
        resourceInfoPanel.SetActive(true);
        foreach (var model in getResource.playerResources)
        {
            if (code.text.Equals(model.code))
            {
                m_ResName.text = model.name.ToUpper();
                m_ResCount.text = model.count.ToString();
                m_ResIcon.sprite = Resources.Load<Sprite>("resources_icon/" + model.code);
                break;
            }
        }
    }
}
