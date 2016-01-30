using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour
{

    public float moveSpeed;
    public float shootMultiplicator = 0.7f;
    public float jumpStrength;
    public float jumpSpeedMult = 1.3f;
    public float timeBetweenBullets = 0.2f;
    public float crouchTime = 5f;


    Rigidbody playerBody;
    Animator animator;

    Transform modelTransform;

    Vector3 speed;
    public Vector3 lookDirection; // public so that we can set the shooting direction in the first level

    float speedMult;
    float crouchMult = 1;
    float shootMult = 0.7f;

    float z;
    float x;

    public bool firstLevel = false;

    bool active = true;
    bool canjump = true;
    float crouch = 0;
    float crouchLeft;
    bool firing = false;

    float timer = 1; // set to 1 so that the first bullet goes off instantly
    int shootCount = 0;



    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        modelTransform = transform.GetChild(0);

        modelTransform.rotation = Quaternion.LookRotation(lookDirection);
    }

    void Update()
    {

        animator.SetBool("Tbagging", false);
        if (active)
        {
            if (!firstLevel)
            {
                z = Input.GetAxisRaw("Vertical");
                x = Input.GetAxisRaw("Horizontal");
                crouch = Input.GetAxisRaw("Fire2");
            }

            if (Input.GetAxis("Fire1") != 0 && !firing)
            {
                firing = true;
            }
        }

        if (firing)
        {
            shootMult = shootMultiplicator;
            Shoot();
        }
        else
        {
            shootMult = 1;
        }

        if (crouch != 0 && canjump)
        {
            crouchMult = 0;
            animator.SetBool("Tbagging", true);
            crouchLeft = crouchTime;
        }
        crouchLeft -= Time.deltaTime;
        if (crouchMult == 0 && crouchLeft <= 0)
        {
            crouchMult = 1;
        }


        if (canjump)
        {
            speed = new Vector3(x, 0, z);
        }
        speed.Normalize();

        if (speed != Vector3.zero)
        {
            lookDirection = speed;
            modelTransform.rotation = Quaternion.LookRotation(lookDirection);
            animator.SetBool("Running", true);
        }
        else
            animator.SetBool("Running", false);

    }

    void FixedUpdate()
    {
        transform.Translate(speed * speedMult * crouchMult * shootMult *  moveSpeed * Time.deltaTime);

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
        animator.SetBool("Shooting", true);
        timer += Time.deltaTime;
        if (timer > timeBetweenBullets)
        {
            GameObject bulletclone = Instantiate(Resources.Load("Prefabs/bullet")) as GameObject;
            bulletclone.transform.position = transform.position + lookDirection;
            bulletclone.transform.rotation = Quaternion.LookRotation(lookDirection);
            bulletclone.GetComponent<BulletScript>().SetDirection(lookDirection);
            shootCount++;
            timer = 0;
        }

        if (shootCount >= 3)
        {
            firing = false;
            shootCount = 0;
            timer = 1;
            animator.SetBool("Shooting", false);
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

