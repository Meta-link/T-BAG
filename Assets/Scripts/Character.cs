using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{

    public float moveSpeed;
    Rigidbody playerBody;

    // Use this for initialization
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        if (z != 0)
        {
            transform.Translate(0, 0, z * moveSpeed * Time.deltaTime);
        }
        if (x != 0)
        {
            transform.Translate(x * moveSpeed * Time.deltaTime, 0, 0);
        }

        if( Input.GetButton("Jump"))
        {
            // faire un beau jump ici
        }
    }
}
