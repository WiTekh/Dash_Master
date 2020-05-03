using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ennemyBehaviour : MonoBehaviour
{
    private GameObject _bullet;
    public GameObject pS;
    public Transform instBullet;

    [SerializeField]
    private float fireRate;
    private float _nextFire = 0;

    public float speed;

    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _bullet = Resources.Load("bullet") as GameObject;
    }

    private void Update()
    {
        // Look at the player
        Vector3 __lookAtTarget = _player.transform.position - transform.position;
        float __angle = Mathf.Atan2(__lookAtTarget.y, __lookAtTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(__angle - 90, Vector3.forward);
        transform.rotation = q;

        // Shoot Mechanic
        if (_nextFire >= fireRate)
        {
            Vector2 bulletPos = instBullet.position;
            Vector2 playerPos = _player.transform.position;
            GameObject __bullet = Instantiate(_bullet, bulletPos, instBullet.rotation);
            __bullet.GetComponent<bulletBehaviour>().speed = this.speed;
            __bullet.GetComponent<bulletBehaviour>().direction = new Vector2(playerPos.x - bulletPos.x, playerPos.y - bulletPos.y).normalized;
            Rigidbody2D __rb = __bullet.GetComponent<Rigidbody2D>();
            __rb.AddForce(__lookAtTarget);
            _nextFire = 0;
        }
        else
        {
            _nextFire += Time.deltaTime;
        }
    }
}
