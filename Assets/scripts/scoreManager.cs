using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scoreManager : MonoBehaviour
{
    public float score;

    private void Update()
    {
        score += Time.deltaTime * 5;

        GetComponent<TMP_Text>().text = "Score :\n" + (int)score;
    }
}
