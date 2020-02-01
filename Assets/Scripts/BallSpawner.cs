using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    public void SpawnBall(float delay)
    {
        StartCoroutine(spawnBall(delay));
    }

    IEnumerator spawnBall(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(ballPrefab, transform.position, Quaternion.identity);
    }
}
