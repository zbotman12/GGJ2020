using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [Header("Component References")]
    public BackgroundLibrary backgroundLibrary;

    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void ParseSetBackgroundEvent(string[] args)
    {
        switch (args.Length)
        {
            case 1:
                SetBackground(args[0]);
                break;
            default:
                throw new System.ArgumentException($"Invalid number of args for Set Background Event.");
        }
    }

    /// <summary>
    /// Changes the background of scene. To do this with transitions, go through the ScreenEffectRenderer
    /// </summary>
    /// <param name="background">the name of the background, found in the background library</param>
    public void SetBackground(string background)
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }

        foreach (BackgroundLibraryElement ble in backgroundLibrary.elements)
        {
            if (ble.name == background)
            {
                image.sprite = ble.background;
            }
        }
    }
}
