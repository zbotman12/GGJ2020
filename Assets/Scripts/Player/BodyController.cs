using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    public BasicMovement movement;
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("XVelocity", transform.InverseTransformDirection(movement.rb.velocity).x);
        anim.SetFloat("YVelocity", transform.InverseTransformDirection(movement.rb.velocity).z);
    }
}
