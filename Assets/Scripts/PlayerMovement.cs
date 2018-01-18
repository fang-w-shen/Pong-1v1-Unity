using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    public float height = 3;
    public Rigidbody rb;
    public string axis = "Vertical";
    /// <summary>
    /// Clamping paddles to gamefield area
    /// </summary>
    void FixedUpdate()
    {
        float velocity = Input.GetAxisRaw(axis);
        rb.position = new Vector3
        (
            rb.position.x,
            Mathf.Clamp(rb.position.y, -height, height),
            0.0f
        );
        rb.velocity = new Vector3(0, velocity, 0) * speed;

    }

}