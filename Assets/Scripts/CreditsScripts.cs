using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreditsScripts : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.Space))
            SceneManager.LoadScene(0);
        if (Input.GetKey(KeyCode.Return))
            SceneManager.LoadScene(0);
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }
}
