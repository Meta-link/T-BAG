using UnityEngine;
using System.Collections;

public class DestroyScript : MonoBehaviour
{

    AudioSource source;
    GameObject Corpse;

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
            gameObject.layer = LayerMask.NameToLayer("Destructed");
            if( Corpse != null)
            {
                print("tralalal");
                Corpse.GetComponent<Rigidbody>().AddForce(0, 1, 0);
            }
            //GetComponent<BoxCollider>().enabled = false;
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.transform.tag == "ennemyForCrate")
        {
            Corpse = collisionInfo.gameObject;
        }
    }
}