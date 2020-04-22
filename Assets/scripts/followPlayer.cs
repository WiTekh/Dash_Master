using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    [SerializeField] private float speed;
    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 LerpedPos = new Vector3(Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * speed).x, Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * speed).y, -10);
        transform.position = LerpedPos;
    }
}
