using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterStage : MonoBehaviour
{
    [SerializeField] List<StagePosition> _positions;

    public Transform GetStagePosition(string name)
    {
        return _positions.Find(x => x.name.Trim().ToLower() == name.Trim().ToLower()).transform;
    }
}
