using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{

    public float moveSpeed;
    public float jumpStrength;
    public float jumpSpeedMult;

    Rigidbody playerBody;

    Transform modelTransform;

    Vector3 speed;
    float speedMult;

    float z;
    float x;

    bool canjump = true;


    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        modelTransform = transform.GetChild(0);
    }

    void Update()
    {
        z = Input.GetAxisRaw("Vertical");
        x = Input.GetAxisRaw("Horizontal");

        if (canjump)
        {
            speed = new Vector3(x, 0, z);
        }
        speed.Normalize();

        if (speed != Vector3.zero)
            modelTransform.rotation = Quaternion.LookRotation(speed);

    }

    void FixedUpdate()
    {
        transform.Translate(speed * speedMult * moveSpeed * Time.deltaTime);

        

        if (Input.GetAxis("Jump") != 0 && canjump)
        {
            playerBody.AddForce(new Vector3(0, jumpStrength, 0));
        }

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

