using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dnew : MonoBehaviour
{
    static Dnew instance = null;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

}
