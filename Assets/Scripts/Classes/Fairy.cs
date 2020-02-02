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
    public GameObject model;
    public BlockBehavior currBlock;

    public void SetDestination(Vector3 dest)
    {
        navAgent.destination = dest;
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

    void FixedUpdate()
    {
        if (currBlock != null && currBlock.currHealth > 0)
        {
            if (Vector3.Distance(transform.position, currBlock.transform.position) < 2)
            {
                currBlock.addHealth(.2f);
            }
        }
        else if (currBlock != null && currBlock.currHealth <= 0)
            currBlock = null;
    }

    public void CheckForNewBlocks()
    {
        BlockBehavior temp = GameManager.instance.FindWeakestWall(leftSide);
        if (temp == null || temp.currHealth <= 0 || temp.currHealth >= 100)
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

    public void SetMaterial(Material mat)
    {
        model.GetComponent<Renderer>().material = mat;
        trail.GetComponent<Renderer>().material = mat;

    }

    public void OnTriggerEnter(Collider coll)
    {
        if (!captured && coll.tag == "Player")
        {
            Player player = coll.gameObject.GetComponent<Player>();
            player.RegisterFairy(this);
            SetMaterial(player.PlayerMaterial);
            captured = true;
            leftSide = player.name == "Player2";
            InvokeRepeating("CheckForNewBlocks", .1f, .5f);
        }
    }
}
