using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int value;
    public string pattern;

    void Awake()
    {
        Init();
    }

    void Init()
    {
        pattern = gameObject.name[5].ToString();
        value = int.Parse(gameObject.name.Substring(7, 2));

        if (value == 14)
            value = 11;

        else if (value > 10)
            value = 10;
    }
}
