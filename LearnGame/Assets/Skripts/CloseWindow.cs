using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseWindow : MonoBehaviour
{
    public GameObject window;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            Destroy(window);
        });
    }
}
