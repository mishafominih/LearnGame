using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WikiManager : MonoBehaviour
{
    public Text Question;
    public Text Link;
    public Button Url;

    private string result;
    void Start()
    {
        var question = Window.Instance.GetValue("question");
        var link = Window.Instance.GetValue("link");
        result = Window.Instance.GetValue("answer");
        Question.text = question;
        Link.text = link;
        Url.onClick.AddListener(() =>
        {
            Application.OpenURL(link);
        });
    }

    public void CheckResult(string str)
    {
        if(str == result)
        {
            Question.text = "Правильно!";
            StartCoroutine(exit());
        }
    }

    private IEnumerator exit()
    {
        yield return new WaitForSeconds(2);
        Window.Instance.CloseWindow();
    }
}
