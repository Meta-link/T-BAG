using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour
{

    public float moveSpeed;
    public float crouchMultiplicator = 0.5f;
    public float jumpStrength;
    public float jumpSpeedMult;

    Rigidbody playerBody;

    Vector3 speed;
    float speedMult;
    float crouchMult = 1;

    float z;
    float x;

    bool active = true;
    bool canjump = true;
    bool crouch = false;


    void Start()
    {
        playerBody = GetComponent<Rigidbody>();

    }

    void Update()
    {
        if(active)
        { 
            z = Input.GetAxisRaw("Vertical");
            x = Input.GetAxisRaw("Horizontal");
            crouch = Input.GetKey(KeyCode.C);
        }

        if (crouch && canjump)
        {
            crouchMult = crouchMultiplicator;
        }

        if (canjump)
        {
            speed = new Vector3(x, 0, z);
        }
        speed.Normalize();
    }

    void FixedUpdate()
    {
        transform.Translate(speed * speedMult * crouchMult * moveSpeed * Time.deltaTime);

        if (Input.GetAxis("Jump") != 0 && canjump)
        {
            playerBody.AddForce(new Vector3(0, jumpStrength, 0));
        }

        crouchMult = 1;
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

    void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "ennemy" && crouch)
        {
            Debug.Log("TEABAGGED");
            other.transform.tag = "ennemyDown";
            //other.transform.parent.GetComponent<Renderer>().material = Resources.Load<Material>("BasicBlue.mat");
            GameObject.Find("GameManager").GetComponent<GameManagerScript>().removeEnnemy();
        }

    }

    public void setActive(bool a)
    {
        active = a;
    }

}

