using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
[Serializable]
public class SuccessEvent: UnityEvent { }

public class PasswordRecv : MonoBehaviour
{
    public string target;
    public TextMesh text;
    string buffer;


    public SuccessEvent OnSuccess;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = buffer;
    }
    
    public void Recv(string s)
    {
        buffer += s;
        if (buffer.EndsWith("<confirm>"))
        {
            if (buffer.Equals(target))
            {
                OnSuccess.Invoke();
            } else
            {
                buffer = "";
            }
        }
    }
}
