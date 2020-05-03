using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehaviour : MonoBehaviour
{
    public float speed;
    public Vector2 direction;

    void Start()
    {
        StartCoroutine("Deathtroy");
    }

    private void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = direction  * speed;
    }

    IEnumerator Deathtroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
