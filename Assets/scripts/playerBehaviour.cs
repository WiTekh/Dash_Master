using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviour : MonoBehaviour
{
    [SerializeField] private KeyCode Dash;

    [SerializeField] private float range;
    [SerializeField] private float dashTime;

    void Update()
    {
        Vector3 mouseScreenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log($"Mouse Pos : ({mouseScreenPos.x},{mouseScreenPos.y})");

        //Rotation of the player facing the mouse Position (27 is an Offset)
        if (Camera.main != null)
        {
            float Angle = 180 / Mathf.PI *
                          Mathf.Atan2(mouseScreenPos.y - transform.position.y, mouseScreenPos.x - transform.position.x);
            transform.rotation = Quaternion.Euler(0, 0, Angle + 27);
        }
        else
            throw new Exception("Camera doesn't exist :(");
        
        //Dash mechanic
        if (Input.GetKeyDown(Dash))
        {
            Vector3 playRelative = mouseScreenPos - transform.position;
            Vector3 lerp = Vector3.Lerp(transform.position, transform.position + playRelative.normalized * range, Time.deltaTime * dashTime);
            transform.position = new Vector3(lerp.x, lerp.y, 0);
        }
    }
}
