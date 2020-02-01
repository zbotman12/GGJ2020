using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeeShooterCollision : MonoBehaviour
{
    public SphereCollider sc;
    public void Start()
    {
        StartCoroutine(CollisionDelay());
    }
    IEnumerator CollisionDelay()
    {
        yield return new WaitForSeconds(.4f);
        sc.enabled = true;
    }
    private void OnCollisionEnter(Collision coll)
    {
        switch (coll.gameObject.tag)
        {
            case "Player":
                // Maybe do a stun or somthing?
                coll.gameObject.GetComponent<BasicMovement>().Stun(0.2f);                
                break;
            case "Wall":
                coll.gameObject.GetComponent<BlockBehavior>().damage(3);                
                break;            
        }
        Destroy(gameObject);
    }
    public void OnTriggerEnter(Collider coll)
    {
        print(coll.name);
    }
}
