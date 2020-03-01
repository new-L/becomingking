using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NumberConversion : MonoBehaviour
{
    //Конвертируем цифры для вывода на экран
    public string NumberConverter(long number)
    {
        float convertToDecimal = 0;
        string kvalue = "";
        if (number < 1000) kvalue = number.ToString();
        else if (number >= 1000 && number <= 1000000) { convertToDecimal = (float)Math.Round((float)number / 1000, 1); kvalue = convertToDecimal.ToString() + "k"; }
        else if (number >= 1000000 && number < 1000000000) { convertToDecimal = (float)Math.Round((float)number / 1000000, 1); kvalue = convertToDecimal.ToString() + "kk"; }
        else if (number >= 1000000000 && number < 1000000000000) { convertToDecimal = (float)Math.Round((float)number / 1000000000, 1); kvalue = convertToDecimal.ToString() + "kkk"; }
        return kvalue;
    }

}
