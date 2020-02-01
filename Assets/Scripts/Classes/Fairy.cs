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

    public BlockBehavior currBlock;

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
        if (currBlock != null && currBlock.currHealth > 0)
        {
            if (Vector3.Distance(transform.position, currBlock.transform.position) < 2)
            {
                currBlock.currHealth += 0.1f;
            }
        }
        else if (currBlock != null && currBlock.currHealth <= 0)
            currBlock = null;
    }

    public void CheckForNewBlocks()
    {
        BlockBehavior temp = GameManager.instance.FindWeakestWall(leftSide);
        if (temp.currHealth <= 0 || temp.currHealth >= 100)
        {
            navAgent.destination = new Vector3(leftSide ? -7 : 7, 1, 0);
        }
        else
        {
            currBlock = temp;
            Vector3 tempPos = currBlock.transform.position;
            tempPos.y = 1;
            navAgent.destination = tempPos;
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
            InvokeRepeating("CheckForNewBlocks", .1f, .5f);
        }
    }
}
