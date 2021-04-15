using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonopolyWindow : Window
{
    private MonopolyPlayer player;

    public void StartPlay(string path, MonopolyPlayer player)
    {
        this.player = player;
        var jsonFile = Resources.Load("Jsons\\" + path).ToString();
        var info = JSON.Parse(jsonFile);
        windows = info["windows"];
    }

    //public static new MonopolyWindow Instance;
    protected void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!isActiveWindow() && curent != -1)
        {
            curent = -1;
            windows = null;
            oneStep = true;
            if(result == 1)
            {
                Game.Instance.RegisterAnswer(player);
                result = 0;
            }
            return;
        }
        if (windows != null && !isActiveWindow() && oneStep)
        {
            curent = Random.Range(0, windows.Count);
            var window = windows[curent]["window_" + curent];
            var type = RemoveQuote(window["type"]);
            var prefab = (GameObject)Resources.Load("Windows\\" + type);
            Instantiate(prefab, transform);
            oneStep = false;
        }
    }
}
