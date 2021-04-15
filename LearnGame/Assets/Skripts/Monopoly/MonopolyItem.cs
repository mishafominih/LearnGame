using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonopolyItem : MonoBehaviour
{
    public bool OnStart = false;
    public Color StartColor;

    private void Start()
    {
        StartColor = GetColor();
    }

    public Color GetColor()
    {
        return GetComponent<Image>().color;
    }
}
