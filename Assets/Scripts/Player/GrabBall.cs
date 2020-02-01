﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBall : MonoBehaviour
{
    public string buttonName = "Interact";
    public GameObject playerHandLocation;
    public SphereCollider triggerZone;
    private Ball ball;
    public void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Ball")
        {
            ball = coll.gameObject.GetComponent<Ball>();
            ball.gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }

    public void LateUpdate()
    {
        if (ball != null)
        {
            ball.transform.position = playerHandLocation.transform.position;
            if (Input.GetAxis(buttonName) > 0)
            {                
                ball.Throw(8, playerHandLocation.transform.forward);
                ball.gameObject.GetComponent<SphereCollider>().enabled = true;
                ball = null;
                StartCoroutine(CoolDown());
            }
        }
    }

    IEnumerator CoolDown()
    {
        triggerZone.enabled = false;
        yield return new WaitForSeconds(.5f);
        triggerZone.enabled = true;
    }
}
