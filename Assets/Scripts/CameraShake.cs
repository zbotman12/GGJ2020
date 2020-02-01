﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    public float shakeTimeAmt = 0.3f, shakeAmt = 1.1f;
    public bool debugMode = false;
    private float timer; 
    private Vector3 originalPos;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
            Destroy(this);

        if (debugMode)
            ShakeCamera();
    }
    public void ShakeCamera()
    {
        originalPos = transform.position;
        timer = 0;
        StartCoroutine(ScreenShake());
    }

    private IEnumerator ScreenShake()
    {
        while (timer < shakeTimeAmt)
        {
			transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmt;			
			timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
		}
			transform.localPosition = originalPos;
    }
}
