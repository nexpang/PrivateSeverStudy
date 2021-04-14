using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatDataObject
{
    public string sender{get; private set;}
    public string msg {get; private set;}

    public ChatDataObject(string data){
        int idx = data.IndexOf(":");
        sender = data.Substring(0, idx);
        msg = data.Substring(idx+1);
    }
}
