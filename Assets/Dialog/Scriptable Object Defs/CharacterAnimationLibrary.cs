using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterAnimation
{
    public string name;
    [Tooltip("Root Motion Animation Clip. Must start and stop on the same frame in order to avoid changing the character's final positions.")]
    public AnimationClip animation;
}

[CreateAssetMenu(fileName = "New Character Animation Library", menuName = "Dialog/Create New Character Animation Library", order = 1)]
public class CharacterAnimationLibrary : ScriptableObject
{
    public List<CharacterAnimation> elements;
}
