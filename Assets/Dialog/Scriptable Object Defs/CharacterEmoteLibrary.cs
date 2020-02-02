using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Emote Library", menuName = "Dialog/Create New Character Emote Library", order = 1)]
public class CharacterEmoteLibrary : ScriptableObject
{
    [SerializeField] CharacterSpriteElement[] emotions;
    Dictionary<string, Sprite> emotionDict;

    void Awake()
    {
        if (emotionDict == null)
        {
            emotionDict = new Dictionary<string, Sprite>();
            foreach (CharacterSpriteElement c in emotions)
            {
                emotionDict.Add(c.name.ToLower(), c.sprite);
            }
        }
    }

    // overloads the square brackets to provide array-like access.
    public Sprite this[string name]
    {
        get
        {
            Awake();
            return emotionDict[name.ToLower()];
        }
    }

    public int Length
    {
        get
        {
            return emotionDict.Count;
        }
    }

    public int Count => Length;
}
