using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] Vector3 MaxSize;
    [SerializeField] float explodeDamage = 10;
    [SerializeField] float stunDuration = 2;

    void Start()
    {
        StartCoroutine(blowUp(0.4f));
    }

    // Update is called once per frame
    IEnumerator blowUp(float duration)
    {
        float time = 0;
        while(time < duration)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, MaxSize, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        FindObjectOfType<BallSpawner>().SpawnBall(2);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        var wall = other.transform.gameObject.GetComponent<BlockBehavior>();
        if (wall != null)
        {
            wall.damage(explodeDamage);
        }

        if(other.tag =="Player")
        {
            other.transform.gameObject.GetComponent<BasicMovement>().Stun(stunDuration);
        }
    }
}
