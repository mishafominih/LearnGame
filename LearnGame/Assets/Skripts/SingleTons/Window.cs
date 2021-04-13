using System;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.Linq;

public class Window : MonoBehaviour
{
    public static Window Instance;

    private JSONNode windows;
    private int curent = -1;
    private bool oneStep = true;
    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path">путь до файла без расширения</param>
    public void StartPlay(string path)
    {
        var jsonFile = Resources.Load("Jsons\\" + path).ToString();
        var info = JSON.Parse(jsonFile);
        windows = info["windows"];
    }

    private void Update()
    {
        if (isActiveWindow()) oneStep = true;
        if (windows != null && !isActiveWindow() && oneStep)
        {
            curent += 1;
            if (curent < windows.Count)
            {
                var window = windows[curent]["window_" + curent];
                var type = RemoveQuote(window["type"]);
                var prefab = (GameObject)Resources.Load("Windows\\" + type);
                Instantiate(prefab, transform);
                oneStep = false;
            }
            else
            {
                windows = null;
                curent = -1;
            }
        }
    }

    private static string RemoveQuote(JSONNode value)
    {
        return value
            .ToString()
            .Split(new char[] { '"' }, StringSplitOptions.RemoveEmptyEntries)
            .First();
    }

    public string GetValue(string key)
    {
        var window = windows[curent]["window_" + curent];
        var data = window[key];
        return RemoveQuote(data);
    }

    private bool isActiveWindow()
    {
        return transform.childCount != 0;
    }

    public void CloseWindow()
    {
        if (transform.childCount != 0)
            Destroy(transform.GetChild(0).gameObject);
    }
}
