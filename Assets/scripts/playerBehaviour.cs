using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// TO DO LIST
/// Add :
///     Health
///     AI
///     Death condition
///     Win condition
///     time with endless mode
/// Modifications :
///     PostProcessing Enhancements
///     Screenshake
///     
/// </summary>
public class playerBehaviour : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int _currentHealth;
    [SerializeField] private KeyCode Dash;

    public GameObject[] healthUI = new GameObject[2];

    private GameObject _trail;
    private Camera _camera;

    private int _dealt = 0;
    private bool _dead = false;

    public bool dashUpped = false;

    void Start()
    {
        //Getting the UI elements
        healthUI[0] = GameObject.Find("health_01").gameObject;
        healthUI[1] = GameObject.Find("health_02").gameObject;

        _camera = Camera.main;
        _trail = transform.GetChild(0).gameObject;

        //Initialisation of health stat and linked UI
        _currentHealth = maxHealth;
        foreach (var h in healthUI)
        {
            h.gameObject.SetActive(true);
        }

        gameObject.tag = "Player";
    }
    void Update()
    {
        float dSize = 0.7f;

        if (dashUpped)
            dSize = 0.2f;
        
        //UI and Health Gestion
        int lackingHealth = maxHealth - _currentHealth;

        //Rotation of the player making it facing the mouse
        Vector3 mouseScreenPos = _camera.ScreenToWorldPoint(Input.mousePosition);

        if (_camera != null)
        {
            var dir = Input.mousePosition - _camera.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        }
        else
            throw new Exception("Camera doesn't exist :(");

        //Dash at the mouse's position
        if (Input.GetKeyDown(Dash))
        {
            transform.DOMove(new Vector2(mouseScreenPos.x, mouseScreenPos.y), 0.25f);
            StartCoroutine(backN());
            transform.DOScale(new Vector3(dSize, 1), 0.1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Particle Gestion
        if (other.gameObject.CompareTag("Ennemy"))
        {
            Destroy(other.gameObject);
            GameObject oo = Instantiate(other.gameObject.GetComponent<ennemyBehaviour>().pS, other.transform.position,
                Quaternion.identity);
            oo.GetComponent<ParticleSystem>().Play();
            Destroy(oo, 0.5f);
            GameObject.Find("Score").GetComponent<scoreManager>().score += 100;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If Shot
        if (other.gameObject.CompareTag("Bullet"))
        {
            Damage();
            Destroy(other.gameObject);
        }

        // If Picked Up Heal
        if (other.gameObject.CompareTag("heal"))
        {
            Heal();
            Destroy(other.gameObject);
        }
        
        // If Picked Up Clock
        if (other.gameObject.CompareTag("clock"))
        {
            StartCoroutine("slowTime", false);
            Destroy(other.gameObject);
        }
        
        // If Smol Upgrade
        if (other.gameObject.CompareTag("smol"))
        {
            dashUpped = true;
            Destroy(other.gameObject);
        }
    }

    void Heal()
    {
        GameObject __hp2 = healthUI[0];
        GameObject __hp1 = healthUI[1];

        if (_dealt <= 0)
            _dealt = 0;
        else
        {
            switch (_dealt)
            {
                case 1:
                    Debug.Log("Reached case 0");
                    __hp2.SetActive(true);
                    break;
            }
        }

        _dealt -= 1;
    }

    void Damage()
    {
        GameObject __hp2 = healthUI[0];
        GameObject __hp1 = healthUI[1];

        _dealt += 1;

        switch (_dealt)
        {
            case 1:
                __hp2.SetActive(false);
                break;
            case 2:
                __hp1.SetActive(false);
                Death();
                break;
        }
    }

    private void Death()
    {
        _dead = true;
        StartCoroutine("slowTime", true);
        Debug.Log("Player is dead");
        foreach (var b in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Destroy(b);
        }
    }

    IEnumerator slowTime(bool d)
    {
        float slowRate = 0;
        float min = 0;
        if (d)
            slowRate = 0.1f;
        else
        {
            slowRate = 0.2f;
            min = 0.2f;
        }

        for (float i = 1; i >= min; i -= slowRate)
        {
            Time.timeScale = i;
            yield return new WaitForSecondsRealtime(0.2f);
        }

        if (!d)
        {
            yield return new WaitForSecondsRealtime(5f);
            Time.timeScale = 1;
        }
    }

    IEnumerator backN()
    {
        yield return new WaitForSeconds(0.25f);
        transform.DOScale(new Vector3(1, 1), 0.05f);
    }
}