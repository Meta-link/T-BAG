using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {


    public float timeBetweenBullets = 0.5f;
    public GameObject bullet; // put the bullet prefab here


    bool firing = false;
    float timer = 1; // set to 1 so that the first bullet goes off instantly
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
                GameObject bulletclone = Instantiate(Resources.Load("Prefabs/bullet")) as GameObject;
                bulletclone.transform.position = transform.position + this.GetComponent<CharacterScript>().GetDirection() ;
                bulletclone.transform.rotation = Quaternion.LookRotation(this.GetComponent<CharacterScript>().GetDirection()); 
                bulletclone.GetComponent<BulletScript>().SetDirection(this.GetComponent<CharacterScript>().GetDirection());
                shootCount++;
                timer = 0;
                print("fire");
            }

            if(shootCount >= 3)
            {
                firing = false;
                shootCount = 0;
                timer = 1;
            }

        }

	}
}
