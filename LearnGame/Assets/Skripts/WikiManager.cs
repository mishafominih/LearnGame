using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WikiManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var data = Window.Instance.GetData();
        transform.GetChild(0).GetComponent<Text>().text = data[0];
        transform.GetChild(1).GetComponent<Text>().text = data[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
