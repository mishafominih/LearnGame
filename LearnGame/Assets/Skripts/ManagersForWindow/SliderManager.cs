using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public Text Question;
    public Image Image;
    public Text Massege;
    public Image Background;

    private bool result;
    private string wrongMassage;
    private string rightMassage;
    void Start()
    {
        var question = Window.Instance.GetValue("question");
        var imagePath = Window.Instance.GetValue("image");
        result = bool.Parse(Window.Instance.GetValue("result"));
        wrongMassage = Window.Instance.GetValue("wrongMassage");
        rightMassage = Window.Instance.GetValue("rightMassage");
        var image = Resources.Load<Sprite>("Sprites\\" + imagePath);
        Question.text = question;
        Image.sprite = image;
    }

    public void Result(bool isRight)
    {
        if (result == isRight) Massege.text = rightMassage;
        else Massege.text = wrongMassage;
        Background.gameObject.SetActive(false);
    }
}
