using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterMovementTypeAsset : ScriptableObject
{
    [SerializeField] AnimationCurve xPosition;
    [SerializeField] AnimationCurve yPosition;
}
