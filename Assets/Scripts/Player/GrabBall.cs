using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBall : MonoBehaviour
{
    public string buttonNameAlt = "InteractButton";
    public string buttonName = "Interact";
    public GameObject playerHandLocation;
    public GameObject peeShooterPrefab;
    public SphereCollider triggerZone;
    public bool isLeftSide;
    [SerializeField]
    private Ball ball;
    public bool usedTrigger = false, canPickUp = true;
    bool enableBallThrow = false, shootable = true;
    public Animator ThrowController;
    public void OnTriggerStay(Collider coll)
    {
        if (Input.GetAxis(buttonName) > 0)
        {
            usedTrigger = true;
        }
        if (coll.gameObject.tag == "Ball" && (Input.GetAxis(buttonName) > 0 || Input.GetButtonDown(buttonNameAlt)) && ball == null && canPickUp)
        {
            canPickUp = false;
            ball = coll.gameObject.GetComponent<Ball>();
            ball.gameObject.GetComponent<SphereCollider>().enabled = false;
            enableBallThrow = false;
        }
    }

    public void Update()
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
            if (Input.GetAxis(buttonName) > 0 && enableBallThrow || Input.GetButton(buttonNameAlt) && enableBallThrow)
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
                ThrowController.SetTrigger("Throw");
                shootable = false;
                Vector3 spawnPosition = transform.position;
                spawnPosition.y = 1;
                GameObject pee = Instantiate(peeShooterPrefab, spawnPosition, Quaternion.Euler(0, isLeftSide? 180: 0, 0));
                Destroy(pee, 12);
                pee.GetComponent<Rigidbody>().velocity = playerHandLocation.transform.forward * 8;
                StartCoroutine(PeeShooterCooldown());
            }
        }
    }

    IEnumerator CoolDown()
    {        
        yield return new WaitForSeconds(1f);
        triggerZone.enabled = true;
        canPickUp = true;
    }
    IEnumerator PeeShooterCooldown()
    {
        yield return new WaitForSeconds(.3f);
        shootable = true;
    }
}
