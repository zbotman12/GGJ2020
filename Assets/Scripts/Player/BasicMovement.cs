using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    public bool playerLeft;
    public string HorizontialAxis, VerticalAxis;

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis(HorizontialAxis);
        float moveVertical = Input.GetAxis(VerticalAxis);

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed * Time.deltaTime * 60);

        if (playerLeft && transform.position.x >= 0)
        {
            Vector3 speedRef = rb.velocity;
            speedRef.x = 0;
            rb.velocity = speedRef;
            Vector3 pos = transform.position;
            pos.x = 0;
            transform.position = pos;
        }
        else if (!playerLeft && transform.position.x <= 0)
        {
            Vector3 speedRef = rb.velocity;
            speedRef.x = 0;
            rb.velocity = speedRef;
            Vector3 pos = transform.position;
            pos.x = 0;
            transform.position = pos;
        }
    }
}
