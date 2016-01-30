using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{

    public float moveSpeed;
    public float jumpStrength;
    public float jumpSpeedMult;

    Rigidbody playerBody;

    Vector3 speed;
    float speedMult;

    bool canjump = true;


    void Start()
    {
        playerBody = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        if (canjump)
        {
            speed = new Vector3(x, 0, z);
        }
        speed.Normalize();

        transform.Translate(speed * speedMult * moveSpeed * Time.deltaTime);

        if (Input.GetAxis("Jump") != 0 && canjump)
        {
            playerBody.AddForce(new Vector3(0, jumpStrength, 0));
        }

    }

    void OnCollisonEnter(Collision collisionInfo)
    {

    }

    void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.transform.tag == "ground")
        {
            canjump = true;
            speedMult = 1;
        }
        else if (collisionInfo.transform.tag == "wall")
        {
            speed = new Vector3(0, 0, 0);
            print("wall2");
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.transform.tag == "ground")
        {
            canjump = false;
            speedMult = jumpSpeedMult;
        }

    }
}

