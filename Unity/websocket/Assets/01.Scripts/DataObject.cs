using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataObject
{
    public string op {get; private set;}
    public string payload {get; private set;}

    public DataObject(string data){
        int idx = data.IndexOf(":");
        op = data.Substring(0, idx);
        payload = data.Substring(idx+1);

    }
}
