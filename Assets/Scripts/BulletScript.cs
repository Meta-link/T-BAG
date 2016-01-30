﻿using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{

    public float bulletSpeed;
    public Vector3 direction;

    float lifetime;

    Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        lifetime += Time.deltaTime;
        if( lifetime >= 15)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = direction * bulletSpeed * Time.deltaTime;
  //      transform.position += direction * bulletSpeed * Time.deltaTime;
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.transform.tag != "Player")
        {
            Destroy(this.gameObject);
        }
    }

}
