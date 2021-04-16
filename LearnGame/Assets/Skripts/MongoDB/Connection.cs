using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour
{ 
    void Start()
    {
        WorkMogoDb.Connection();
        var result = WorkMogoDb.GetData("WhyId", @"D:\Example");
    }
}
