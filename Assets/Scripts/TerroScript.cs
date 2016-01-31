using UnityEngine;
using System.Collections;

public class TerroScript : MonoBehaviour {

    private GameObject panneau;

	// Use this for initialization
	void Start () {
        panneau = transform.FindChild("panneau").gameObject;
        panneau.transform.eulerAngles = new Vector3(0, panneau.transform.eulerAngles.y + Random.Range(-30, 30), 0);
        panneau.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
    }

    void FixedUpdate()
    {
        panneau.GetComponent<Rigidbody>().AddForce(new Vector3(0f, -10f, 0f), ForceMode.Impulse);
    }
}
