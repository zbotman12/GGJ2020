using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody rb;
    public LayerMask collisionLayer;
    public void FixedUpdate()
    {
        Debug.DrawRay(transform.position, rb.velocity, Color.red, Time.deltaTime, false);
        if (Physics.Raycast(transform.position, rb.velocity, out RaycastHit hit, .35f, walls))
        {
            Collision(hit);
        }
    }
    public void Collision(RaycastHit hit)
    {
        float speed = rb.velocity.magnitude;
        var dir = Vector3.Reflect(rb.velocity.normalized, hit.normal);
        rb.velocity = dir * speed;
    }
    void Shoot(float speed, Vector3 direction)
    {     
        rb.velocity = direction.normalized * speed;
    }
}
