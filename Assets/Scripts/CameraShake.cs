using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    [SerializeField]
    private float shakeTimeAmt = 0.5f, shakeAmt = 0.2f;
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
    public void ShakeCamera(float shake)
    {
        originalPos = transform.position;
        timer = 0;
        StartCoroutine(ScreenShake(shake));
    }
    public void StopScreenShake()
    {
        timer = shakeTimeAmt+1;
        StopCoroutine(ScreenShake());
        transform.localPosition = originalPos;
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
    private IEnumerator ScreenShake(float shake)
    {
        while (timer < shakeTimeAmt)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * shake;
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.localPosition = originalPos;
    }
}
