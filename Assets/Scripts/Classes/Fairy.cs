using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fairy : MonoBehaviour
{
    public float range = 10.0f;
    public NavMeshAgent navAgent;
    public bool captured = false, leftSide = false;
    public ParticleSystem trail;

    public void Start()
    {
        navAgent.destination = new Vector3(0, 1, 0);
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    void Update()
    {
        /*
        Vector3 point;        
        if (RandomPoint(transform.position, range, out point))
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
        }
        */

        if (Vector3.Distance(navAgent.destination, transform.position) > 1.0f)
        {
            // Chill here
        }
    }

    public void OnTriggerEnter(Collider coll)
    {
        if (!captured && coll.tag == "Player")
        {
            GameObject player = coll.gameObject;
            gameObject.GetComponent<Renderer>().material = player.GetComponent<Player>().playerMaterial;
            trail.GetComponent<Renderer>().material = player.GetComponent<Player>().playerMaterial;
            captured = true;
            leftSide = player.name == "Player2";
            BlockBehavior temp = GameManager.instance.FindWeakestWall(leftSide);
            if (temp.currHealth <= 0 || temp.currHealth >= 100)
            {
                navAgent.destination = new Vector3(leftSide ? -7 : 7, 1, 0);
            }
            else
            {
                Vector3 tempPos = temp.transform.position;
                tempPos.y = 1;
                navAgent.destination = tempPos;
            }
        }
    }
}
