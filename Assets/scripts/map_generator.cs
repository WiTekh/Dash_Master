using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map_generator : MonoBehaviour
{
    public int size;

    public Vector2 roomSize;
    public Vector2 offset;

    public int[,] map;

    private bool _firstGen = true;
    
    private void Start()
    {
        //Adjusting room size
        if (size % 2 == 0)
            size++;
        
        //map initialisation
        map = new int[size,size];
        offset = Vector2.zero;
        
        //Game Initialisation
        Generate();
    }

    public void Generate()
    {
        int __mid = (size - 1) / 2 + 1;

        for (int x = 0; x <= size; x++)
        {
            for (int y = 0; y <= size; y++)
            {
                //Spawning the player at the center of the map
                if ((x, y) == (__mid, __mid) && _firstGen) {
                    Instantiate(Resources.Load("player"), roomSize * new Vector2(x,y) + offset, Quaternion.identity);
                    Instantiate(Resources.Load("spawn"), roomSize * new Vector2(x, y) + offset, Quaternion.identity);
                    _firstGen = false;
                }
                //Generate Room on the right of the player
                else if (y == __mid && x > __mid) 
                { 
                    GameObject __room = Instantiate(Resources.Load("hor_room"), roomSize * new Vector2(x,y) + offset, Quaternion.identity) as GameObject;
                    Debug.Log(roomSize * new Vector2(x,y) + offset);
                    //Is the room spawned at the end of the matrix
                    if (x == size) 
                    {
                        __room.GetComponent<roomInfos>().isMarked = true;
                    }
                }
            }
        }
    }
}