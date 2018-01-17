using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightWallCollision : MonoBehaviour {

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Ball")
        {
            FindObjectOfType<GameManager>().Player2AddScore();
            FindObjectOfType<GameManager>().ResetBall();
        }
    }
}
