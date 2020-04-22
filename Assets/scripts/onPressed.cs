using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class onPressed : MonoBehaviour
{
    public void ClickPlay()
    {
        SceneManager.LoadScene(1);
    }
}
