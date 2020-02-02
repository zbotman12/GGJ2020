using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dnew : MonoBehaviour
{
    static Dnew instance = null;
    void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
    }

}
