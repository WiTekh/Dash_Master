using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class roomInfos : MonoBehaviour
{
    public bool isMarked = false;
    private Random rd = new Random();
    
    private void Start()
    {
        GetComponent<BoxCollider2D>().enabled = isMarked;
        int id = rd.Next(2, 11);

        transform.GetChild(id).gameObject.SetActive(true);

        for (int i = 0; i < transform.GetChild(id).childCount; i++)
        {
            Instantiate(Resources.Load("ennemy"), transform.GetChild(id).GetChild(i));
        }

        //1 chance / 7 to have an object in this room
        if (rd.Next(8) == 1)
        {
            int oo = rd.Next(3);
            if (oo == 0)
            {
                if (GameObject.Find("player(Clone)").GetComponent<playerBehaviour>().dashUpped)
                    Instantiate(Resources.Load("heal"), transform);
                else
                    Instantiate(Resources.Load("smol"), transform);
            } else if (oo == 1) {
                Instantiate(Resources.Load("clock"), transform);
            } else {
                Instantiate(Resources.Load("heal"), transform);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            map_generator map = GameObject.Find("mapGen").GetComponent<map_generator>();
            map.offset += new Vector2(16,0);
            map.Generate();
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
