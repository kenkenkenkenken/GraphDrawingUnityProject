using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var d = MessageShow();
        d("test", "2");
    }

    public Action<string, string> MessageShow()
    {
        //Action testString = () => Debug.Log("x");
        //return testString;
        return (x, y) => Debug.Log(x);
    }
}
