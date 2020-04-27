using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemsCount : MonoBehaviour
{
    public Text
        _coinCount,
        _chessCount;
    public Image
        _coinIcon,
        _chessIcon;

    private void Start()
    {
        SetColorIcon(_coinIcon, 255, 255, 255, 130);
        SetColorIcon(_chessIcon, 255, 255, 255, 130);
    }
    public void SetText(int count, Text text) 
    {
        text.text = count.ToString() + 'x';
    }

    public void SetColorIcon(Image icon, byte r, byte g, byte b, byte a)
    {
        icon.color = new Color32(r, g, b, a);
    }
}
