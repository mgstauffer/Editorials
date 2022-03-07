using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    
    // Start is called before the first frame update
    public void Start()
    {
        Debug.Log("Start");
    }

    // Update is called once per frame
    public void Update()
    {
        Debug.Log("Update");
    }

    public void MethodInvokeryay(string input, int repeat, bool isError)
    {
        if(repeat > 0)
        {
            for (int i = 0; i < repeat; i++)
            {
                if (!isError)
                {
                    Debug.Log(input);
                }
                else
                {
                    Debug.LogError(input);
                }
            }
        }
        else
        {
            Debug.Log("Boring");
        }
        
    }

    public void CreateObject(string name, Transform parent)
    {
        GameObject go = new GameObject(name);
        go.transform.parent = parent;   
    }
}
