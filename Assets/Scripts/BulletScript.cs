using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{

    public float bulletSpeed;
    public Vector3 direction;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += direction * bulletSpeed * Time.deltaTime;
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

}
