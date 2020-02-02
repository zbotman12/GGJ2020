using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MusicLibraryElement
{
    public string name;
    public AudioClip clip;
}

[CreateAssetMenu(fileName = "New Music Library", menuName = "Dialog/Create New Music Library", order = 1)]
public class MusicLibrary : ScriptableObject
{
    public List<MusicLibraryElement> music;

    public AudioClip this[string key]
    {
        get
        {
            Debug.Log(music.Find((x) => x.name.ToLower().Trim() == key.ToLower().Trim()).name);
            return music.Find((x) => x.name.ToLower().Trim() == key.ToLower().Trim()).clip;
        }
    }

    public int Length
    {
        get
        {
            return music.Count;
        }
    }


}
