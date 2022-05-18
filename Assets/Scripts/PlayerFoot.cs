using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoot : MonoBehaviour
{
    public bool isBlocked = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("triggered?");
        if (other.gameObject.tag == "BlockingFeature")
        {
            Debug.Log("triggered!");
            isBlocked = true;
        }
    }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.gameObject.tag == "BlockingFeature")
    //     {
    //         isBlocked = false;
    //     }
    // }
}
