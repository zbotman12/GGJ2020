using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePosition : MonoBehaviour
{
    //StagePosition may potentally hold sorting layer information

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position + Vector3.up * 1f, transform.position - Vector3.up * 15f);
        Gizmos.DrawLine(transform.position + Vector3.right * 1f, transform.position - Vector3.right * 1f);
        //Gizmos.DrawIcon(transform.position - transform.forward * 0.01f, "CharacterIcon.png");
    }
}
