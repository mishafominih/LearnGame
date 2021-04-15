using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int count;
    private void Start()
    {
        var text = GetComponent<Text>().text;
        count = int.Parse(text);
    }

    public void Increment()
    {
        count += 1;
        WriteScore();
    }

    public void Decrement()
    {
        count -= 1;
        WriteScore();
    }

    private void WriteScore()
    {
        GetComponent<Text>().text = count.ToString();
    }
}
