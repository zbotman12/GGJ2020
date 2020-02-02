using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RedBlueGames.Tools.TextTyper;

[RequireComponent(typeof(Button), typeof(Image))]
public class DialogBox : MonoBehaviour
{
    [SerializeField]
    private AudioClip printSoundEffect;

    public TextMeshProUGUI nameText;
    public bool keepText;
    public bool isDefault;
    public bool clickAnywhere;
    public bool hideWhenInactive = true;
    public bool hideOnAwake = false;

    private TMP_Text m_TextComponent;
    private Coroutine colorFadeRoutine;
    private bool animText;
    //Make sure to thank red blue games when this is released.
    [SerializeField] private TextTyper _typer = null;

    public void Setup(DialogDirector _controller)
    {
        if (!clickAnywhere)
        {
            var b = GetComponent<Button>();
            if (b == null) Debug.Log("Button is null");
            b.onClick.AddListener(_controller.Continue);
            Image i = GetComponent<Image>();
            i.color = Color.clear;
            i.raycastTarget = true;
        }

        if (hideOnAwake)
        {
            gameObject.SetActive(false);
        }
    }

    public bool isTyping()
    {
        return _typer.IsTyping;
    }

    public void Finish()
    {
        _typer.Skip();
    }

    public void DisplayLine(DialogLine storyLine)
    {
        if (nameText != null) nameText.text = storyLine.character;

        _typer.TypeText(storyLine.line);
    }

    public void SetActiveState(bool state)
    {
        gameObject.SetActive(keepText || state);
    }

    private void HandleCharacterPrinted(string printedCharacter)
    {
        // Do not play a sound for whitespace
        if (printedCharacter == " " || printedCharacter == "\n")
        {
            return;
        }

        var audioSource = this.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = this.gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = this.printSoundEffect;
        audioSource.Play();
    }

    private void HandlePrintCompleted()
    {
        Debug.Log("TypeText Complete");
    }

    private void Awake()
    {
        _typer.PrintCompleted.AddListener(HandlePrintCompleted);
        _typer.CharacterPrinted.AddListener(HandleCharacterPrinted);
    }
}