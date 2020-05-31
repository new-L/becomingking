using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResPrefabPointer : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] private ResourcePrefabInfo prefabInfo;
    [SerializeField] private Text code;
    void Start()
    {
        prefabInfo = FindObjectOfType<ResourcePrefabInfo>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        prefabInfo.OnResourceFocus(code);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        prefabInfo.Toggle(code);
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, 171);
    }
}
