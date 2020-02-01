﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBall : MonoBehaviour
{
    public string buttonNameAlt = "InteractButton";
    public string buttonName = "Interact";
    public GameObject playerHandLocation;
    public GameObject peeShooterPrefab;
    public SphereCollider triggerZone;
    [SerializeField]
    private Ball ball;

    bool enableBallThrow = false, usedTrigger = false, shootable = true;
    public void OnTriggerStay(Collider coll)
    {
        usedTrigger = Input.GetAxis(buttonName) > 0;
        if (coll.gameObject.tag == "Ball" && (Input.GetAxis(buttonName) > 0 || Input.GetButtonDown(buttonNameAlt)) && ball == null)
        {
            ball = coll.gameObject.GetComponent<Ball>();
            ball.gameObject.GetComponent<SphereCollider>().enabled = false;
            enableBallThrow = false;
        }
    }

    public void LateUpdate()
    {
        if (ball != null)
        {
            if (Input.GetAxis(buttonName) <= 0 && usedTrigger)
            {
                enableBallThrow = true;
            }
            else if (Input.GetButtonUp(buttonNameAlt))
            {
                enableBallThrow = true;
            }

            // Ball in hand
            ball.transform.position = playerHandLocation.transform.position;
            if ((Input.GetAxis(buttonName) > 0 || Input.GetButtonDown(buttonNameAlt)) && enableBallThrow)
            {
                triggerZone.enabled = false;
                ball.Throw(8, playerHandLocation.transform.forward);
                ball.gameObject.GetComponent<SphereCollider>().enabled = true;
                ball = null;
                StartCoroutine(CoolDown());
            }
        }
        else
        {
            // No Ball in hand
            if ((Input.GetAxis(buttonName) > 0 || Input.GetButton(buttonNameAlt)) && shootable && triggerZone.enabled)
            {
                shootable = false;
                Vector3 spawnPosition = transform.position;
                spawnPosition.y = 1;
                GameObject pee = Instantiate(peeShooterPrefab, spawnPosition, Quaternion.identity);                
                pee.GetComponent<Rigidbody>().velocity = playerHandLocation.transform.forward * 8;
                StartCoroutine(PeeShooterCooldown());
            }
        }
    }

    IEnumerator CoolDown()
    {        
        yield return new WaitForSeconds(.5f);
        triggerZone.enabled = true;
    }
    IEnumerator PeeShooterCooldown()
    {
        yield return new WaitForSeconds(.3f);
        shootable = true;
    }
}
