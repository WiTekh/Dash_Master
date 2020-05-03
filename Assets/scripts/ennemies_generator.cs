using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ennemies_generator : MonoBehaviour
{
    private GameObject ennemy;
    [SerializeField] private float distance;
    
    public Vector3 offset;
    public int nbOE;
 
    private Random rd = new Random();
    
    private void Awake()
    {
        ennemy = Resources.Load("ennemy") as GameObject;
        GenEnnemies(nbOE);
    }

    private void GenEnnemies(int nb)
    {
        float pp = 0f;
        Vector3 place = transform.position;
        for (int i = 0; i <= nbOE; i++)
        {
            //Set the distance between 1 and 7
            float x = (float)rd.NextDouble() * distance;
            float y = (float)Math.Sqrt(Math.Pow(distance, 2) - Math.Pow(x, 2));
            Vector3 rV3 = rd.Next(-1,1)* new Vector3(x, y,0);

            GameObject oo = Instantiate(ennemy, place + rV3, Quaternion.identity);
            place += offset;
            //RenderDistanceGestion
            //oo.SetActive(false);
        }
    }
}
