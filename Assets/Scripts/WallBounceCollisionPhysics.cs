using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBounceCollisionPhysics : MonoBehaviour {

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Ball")
        {
            FindObjectOfType<GameManager>().ReverseVerticalSpeed();
        }
    }

}
