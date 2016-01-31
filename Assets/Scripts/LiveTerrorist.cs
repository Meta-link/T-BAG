using UnityEngine;
using System.Collections;

public class LiveTerrorist : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.transform.tag == "bullet")
        {
            GetComponentInChildren<Animator>().SetTrigger("Killed");
            GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>().firstLevel = false;
            gameObject.layer = LayerMask.NameToLayer("OTG");
            GameObject.Find("TextTuto2").GetComponent<TextMesh>().characterSize = 0.30f;
        }
    }
}
