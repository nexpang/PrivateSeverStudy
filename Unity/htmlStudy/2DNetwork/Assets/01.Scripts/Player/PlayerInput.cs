using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string forntAxisName = "Vertical";
    public string rightAxisName = "Horizontal";
    public string fireButtonName = "Fire1";

    public float frontMove { get; private set; }
    public float rightMove { get; private set; }
    public bool fire { get; private set; }

    public Vector3 mousePos { get; private set; }
    void Update()
    {
        frontMove = Input.GetAxis(forntAxisName);
        rightMove = Input.GetAxis(rightAxisName);
        fire = Input.GetButtonDown(fireButtonName);

        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mp.z = 0;
        mousePos = mp;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color
    }
}
