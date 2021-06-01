using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONTest : MonoBehaviour
{
    void Start()
    {
        string json = "{type:\"CHAT\", payload:\"Hello Unity\"}";
        DataVO vo = JsonUtility.FromJson<DataVO>(json);

        Debug.Log(vo.type);
        Debug.Log(vo.payload);
    }
}
