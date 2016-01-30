using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour
{

    public float moveSpeed;
    public float crouchMultiplicator = 0.5f;
    public float shootMultiplicator = 0.7f;
    public float jumpStrength;
    public float jumpSpeedMult = 1.3f;
    public float timeBetweenBullets = 0.2f;


    Rigidbody playerBody;

    Transform modelTransform;

    Vector3 speed;
    Vector3 lookDirection;

    float speedMult;
    float crouchMult = 1;
    float shootMult = 0.7f;

    float z;
    float x;

    bool active = true;
    bool canjump = true;
    float crouch = 0;
    bool firing = false;

    float timer = 1; // set to 1 so that the first bullet goes off instantly
    int shootCount = 0;



    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        modelTransform = transform.GetChild(0);
    }

    void Update()
    {
        if (active)
        {
            z = Input.GetAxisRaw("Vertical");
            x = Input.GetAxisRaw("Horizontal");
            crouch = Input.GetAxisRaw("Fire2");

            if (Input.GetAxis("Fire1") != 0 && !firing)
            {
                firing = true;
            }
        }

        if (firing)
        {
            Shoot();

        }

        if (crouch != 0 && canjump)
        {
            crouchMult = crouchMultiplicator;
        }

        if (canjump)
        {
            speed = new Vector3(x, 0, z);
        }
        speed.Normalize();

        if (speed != Vector3.zero)
        {
            lookDirection = speed;
            modelTransform.rotation = Quaternion.LookRotation(speed);
        }

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
        else if (collisionInfo.transform.tag == "wall" || collisionInfo.transform.tag == "destructible")
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
        if (other.transform.tag == "ennemy" && crouch != 0)
        {
            Debug.Log("TEABAGGED");
            other.transform.tag = "ennemyDown";
            //other.transform.parent.GetComponent<Renderer>().material = Resources.Load<Material>("BasicBlue.mat");
            Camera.main.GetComponent<ShakeCamera>().DoShake(0.05f);
            GameObject.Find("GameManager").GetComponent<GameManagerScript>().removeEnnemy();
        }

    }

    void Shoot()
    {
        timer += Time.deltaTime;
        if (timer > timeBetweenBullets)
        {
            GameObject bulletclone = Instantiate(Resources.Load("Prefabs/bullet")) as GameObject;
            bulletclone.transform.position = transform.position + lookDirection;
            bulletclone.transform.rotation = Quaternion.LookRotation(lookDirection);
            bulletclone.GetComponent<BulletScript>().SetDirection(lookDirection);
            shootCount++;
            timer = 0;
            print("fire");
        }

        if (shootCount >= 3)
        {
            firing = false;
            shootCount = 0;
            timer = 1;
        }
    }

    public void setActive(bool a)
    {
        active = a;
    }


    public Vector3 GetDirection()
    {
        return lookDirection;
    }
}

