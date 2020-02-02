using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActive : MonoBehaviour
{
    public bool on;
    public void Toggle(GameObject go)
    {
        on = !on;
        go.SetActive(on);
    }
}
