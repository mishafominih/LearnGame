using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public static Window Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void AddWindow(GameObject window)
    {
        if(transform.childCount == 0)
            Instantiate(window, transform);
    }

    public List<string> GetData()
    {
        return new List<string> { "testQuestion", "testLink" };
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
