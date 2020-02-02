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
    Material mat;

    public void Start()
    {
        mat = GetComponent<Renderer>().material;
        collisionLayer = ~collisionLayer;
        scale = gameObject.transform.localScale.x;
        StartCoroutine(blinkred());
    }
    public void FixedUpdate()
    {
        Debug.DrawRay(transform.position, rb.velocity.normalized, Color.red, Time.deltaTime, false);
        if (Physics.Raycast(transform.position, rb.velocity, out RaycastHit hit, 1, collisionLayer))
        {
            Collision(hit);
        }

        //transform.localScale += new Vector3(growRate, growRate, growRate);
        scale += growRate;

        if (scale >= maxScale)
        {
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    IEnumerator blinkred()
    {
        while (scale < maxScale)
        {
            float duration = (maxScale - scale) / 3;
            float timer = 0;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                yield return null;
                mat.SetColor("_Color", Color.Lerp(Color.white,Color.red, timer/duration));
                transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.2f, timer/duration);
            }
            float duration2 = duration/10;
            float timer2 = 0;
            while (timer2 < duration2)
            {
                timer2 += Time.deltaTime;
                yield return null;
                mat.SetColor("_Color", Color.Lerp(Color.red, Color.white, timer2 / duration2));
                transform.localScale = Vector3.Lerp(Vector3.one * 1.2f, Vector3.one , timer2 / duration2);
            }
            transform.localScale = Vector3.one;
            mat.SetColor("_Color", Color.white);
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
    public void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Pee")
            Destroy(coll.gameObject);
    }
    public IEnumerator TurnOnCollider()
    {
        yield return new WaitForSeconds(.2f);
        gameObject.GetComponent<SphereCollider>().enabled = true;
    }
}
