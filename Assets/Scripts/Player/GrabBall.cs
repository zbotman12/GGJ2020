using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBall : MonoBehaviour
{
    public string buttonNameAlt = "InteractButton";
    public string buttonName = "Interact";
    public GameObject playerHandLocation;
    public SphereCollider triggerZone;
    private Ball ball;

    bool enableBallThrow = false;
    bool usedTrigger = false;
    public void OnTriggerStay(Collider coll)
    {
        usedTrigger = Input.GetAxis(buttonName) > 0;
        if (coll.gameObject.tag == "Ball" && (Input.GetAxis(buttonName) > 0 || Input.GetButtonDown(buttonNameAlt)))
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

            ball.transform.position = playerHandLocation.transform.position;
            if ((Input.GetAxis(buttonName) > 0 || Input.GetButtonDown(buttonNameAlt)) && enableBallThrow)
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
