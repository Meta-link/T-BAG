using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {


    public float timeBetweenBullets = 0.5f;
    public GameObject bullet; // put the bullet prefab here


    bool firing = false;
    float timer = 0;
    int shootCount = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetAxis("Fire1")!= 0 && !firing)
        {
            firing = true;
        }

        if (firing)
        {
            timer += Time.deltaTime;
            if(timer > timeBetweenBullets)
            {
                GameObject bulletclone = Instantiate(Resources.Load("bullet")) as GameObject;
                bulletclone.transform.position = transform.position;
                bulletclone.transform.rotation = transform.GetChild(0).rotation;
                shootCount++;
                timer = 0;
                print("fire");
                //GameObject bulletclone = Instantiate(bullet, transform.position, transform.rotation);
            }

            if(shootCount >= 3)
            {
                firing = false;
            }

        }

	}
}
