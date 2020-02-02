using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScreenTransitionLibraryElement
{
    public string name;
    public Texture image;
    public Color color = Color.white;
    public float transitionTime = 0.5f;
    public AnimationCurve easing;
};

[CreateAssetMenu(fileName = "New Screen Transition Library", menuName = "Dialog/Create New Screen Transition Library", order = 1)]
public class ScreenTransitionLibrary : ScriptableObject
{
    public List<ScreenTransitionLibraryElement> elements;
}
