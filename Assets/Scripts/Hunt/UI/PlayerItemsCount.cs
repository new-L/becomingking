using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemsCount : MonoBehaviour
{
    public Text
        _coinCount,
        _chessCount,
        _monsterCount,
        _resourcesCount,
        playerAlert,
        healthPotion,
        energyPotion;
    public Image
        _coinIcon,
        _chessIcon,
        _goldeKey,
        _silverKey,
        _monsters,
        _resources;
    public int monsterCount;
    private GameObject[] enemies;
    [SerializeField] private NumberConversion m_convert;
    private void Start()
    {
        SetColorIcon(_coinIcon, 255, 255, 255, 130);
        SetColorIcon(_chessIcon, 255, 255, 255, 130);
        SetColorIcon(_goldeKey, 255, 255, 255, 130);
        SetColorIcon(_silverKey, 255, 255, 255, 130);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        SetColorIcon(_monsters, 255, 255, 255, 130);
        monsterCount = enemies.Length;
        _monsterCount.text = "x" + monsterCount;
        SetColorIcon(_resources, 255, 255, 255, 130);
    }
    public void SetText(int count, Text text) 
    {
        text.text = count.ToString() + 'x';
    }

    public void SetColorIcon(Image icon, byte r, byte g, byte b, byte a)
    {
        icon.color = new Color32(r, g, b, a);
    }

    public void SetPotionText()
    {
        healthPotion.text = m_convert.NumberConverter(GetItemsCharacter.items.healthpotion).ToString();
        energyPotion.text = m_convert.NumberConverter(GetItemsCharacter.items.energypotion).ToString();
    }
}
