using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    public bool playerLeft;
    public string HorizontialAxis, VerticalAxis;
    public string HorizontialAxisLook, VerticalAxisLook;


    Vector3 velocity;

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis(HorizontialAxis);
        float moveVertical = Input.GetAxis(VerticalAxis);

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        float lookHorizontal = Input.GetAxis(HorizontialAxisLook);
        float lookVertical = Input.GetAxis(VerticalAxisLook);


        Vector3 look = new Vector3(lookHorizontal, 0.0f, lookVertical);
        if (look.magnitude > 0)
        {
            transform.forward = Vector3.SmoothDamp(transform.forward, look, ref velocity, 0.1f);
        }

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

    public void Stun(float duration)
    {
        StartCoroutine(stunTimer(duration));
    }

    IEnumerator stunTimer(float duration)
    {
        enabled = false;
        yield return new WaitForSeconds(duration);
        enabled = true;
    }
}
