using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftWallCollision : MonoBehaviour {

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Ball")
        {
            FindObjectOfType<GameManager>().Player1AddScore();
            FindObjectOfType<GameManager>().ResetBall();
        }
    }
}
