using UnityEngine;
using System.Collections;

public class DestroyScript : MonoBehaviour
{

    AudioSource source;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnDestroy()
    {
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.transform.tag == "bullet")
        {
            Destroy(collisionInfo.gameObject);
            source.Play();
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}