using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleTest : MonoBehaviour
{
    public delegate void AddNumber(int x, int y);

    public event AddNumber addNumvers;
    void Start()
    {
        addNumvers += MyAdd;
        addNumvers += (a, b)=>Debug.Log(a*b);

        addNumvers(3, 4);
    }

    private void MyAdd(int a, int b)
    {
        Debug.Log(a + b);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
