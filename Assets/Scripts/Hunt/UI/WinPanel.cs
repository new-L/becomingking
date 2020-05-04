using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    public Text
        _goldTotal,
        _goldCoin,
        _goldChess,
        _resourceTotal,
        _resourceItem,
        _ratingText;
    public GameObject
        m_Exit;

    public void SetText(Text item, string count, string type)
    {
        switch(type)
        {
            case "gold": item.text = "+" + count + " золота"; break;
            case "resources": item.text = "+" + count + " ресурсов"; break;
            case "rating": item.text = "+" + count + " рейтинга"; break;
        }
    }
}
