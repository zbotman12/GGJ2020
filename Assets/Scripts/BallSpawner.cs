﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] public GameObject ballPrefab;

    GameObject SpawnedBall;
    public void SpawnBall(float delay)
    {
        StartCoroutine(spawnBall(delay));
    }

    IEnumerator spawnBall(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(SpawnedBall);
        SpawnedBall = Instantiate(ballPrefab, transform.position, Quaternion.identity);
    }
}
