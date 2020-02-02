using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BackgroundLibraryElement
{
    public string name;
    public Sprite background;
};

[CreateAssetMenu(fileName = "New Background Library", menuName = "Dialog/Create New Background Library", order = 1)]
public class BackgroundLibrary : ScriptableObject
{
    public BackgroundLibraryElement[] elements;
}
