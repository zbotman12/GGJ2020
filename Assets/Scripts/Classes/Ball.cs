using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody rb;
    public LayerMask collisionLayer;
    public float scale, growRate = .0005f;
    public float maxScale;
    public GameObject ExplosionPrefab;
    public void Start()
    {
        collisionLayer = ~collisionLayer;
        scale = gameObject.transform.localScale.x;
    }
    public void FixedUpdate()
    {
        Debug.DrawRay(transform.position, rb.velocity.normalized, Color.red, Time.deltaTime, false);
        if (Physics.Raycast(transform.position, rb.velocity, out RaycastHit hit, 1, collisionLayer))
        {
            Collision(hit);
        }

        transform.localScale += new Vector3(growRate, growRate, growRate);
        scale = transform.localScale.x;
        if(scale >= maxScale)
        {
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    // Since the raycast wont run on angled hits
    public void OnCollisionEnter(Collision coll)
    {
        if (coll.transform.tag == "Wall")
        {
            if (rb.velocity.magnitude > 4)
                coll.transform.gameObject.GetComponent<BlockBehavior>().damage(33f);
            float speed = rb.velocity.magnitude;
            var dir = rb.velocity;
            rb.velocity = -dir.normalized * (speed / 1.3f);
        }
    }
    public void Collision(RaycastHit hit)
    {
        // We hit a target
        if (hit.transform.tag == "Wall")
        {
            hit.transform.gameObject.GetComponent<BlockBehavior>().damage(rb.velocity.magnitude * 7 * scale);
            float speed = rb.velocity.magnitude;
            var dir = Vector3.Reflect(rb.velocity.normalized, hit.normal);
            rb.velocity = dir * (speed/1.3f);
        }
        else
        {
            float speed = rb.velocity.magnitude;
            var dir = Vector3.Reflect(rb.velocity.normalized, hit.normal);
            rb.velocity = dir * speed;
        }
    }
    public void Throw(float speed, Vector3 direction)
    {     
        rb.velocity = speed * direction.normalized;
    }    
}
