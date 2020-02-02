using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CharacterSpriteElement
{
    public string name;
    public Sprite sprite;
}

public class DialogCharacter : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator animator;
    Dictionary<string, Sprite> emotionDict;

    [SerializeField] CharacterStage _stage;
    public SpriteMask HighlightedEffect;
    public CharacterSpriteElement[] emotions;
    bool selected;

    void Awake()
    {
        emotionDict = new Dictionary<string, Sprite>();
        foreach (CharacterSpriteElement c in emotions)
        {
            emotionDict.Add(c.name.ToLower(), c.sprite);
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    //Need to be able to pass movement type here. 
    //Movement type should probably contain all timing information as well as curve stuff
    //Best to define timing information as speed or time to complete movement??
    public void ShowCharacter(string emote, string stagePosition, bool changeSpriteAfterAnim = false)
    {
        if (stagePosition != "")
        {
            var _targetTransform = _stage.GetStagePosition(stagePosition);
            transform.SetPositionAndRotation(_targetTransform.position, _targetTransform.rotation);
            transform.localScale = _targetTransform.localScale;
        }
        spriteRenderer.enabled = true;
        SetSprite(emote);
    }

    bool hidden = false;
    public void HideCharacter(string emote, string animation)
    {
        hidden = true;
        SetSprite(emote);
        HighlightedEffect?.gameObject.SetActive(false);
        selected = false;
        animator.Play(animation);
    }

    IEnumerator doHighlight(float delay, bool value)
    {
        yield return new WaitForSeconds(delay);
        if (value && !hidden && spriteRenderer.enabled) HighlightedEffect?.gameObject.SetActive(value);
    }

    public void SetSprite(string emote)
    {
        if(emotionDict.ContainsKey(emote.ToLower()))
        {
            spriteRenderer.sprite = emotionDict[emote.ToLower()];
            if (HighlightedEffect != null && spriteRenderer.enabled)
            {
                HighlightedEffect.sprite = emotionDict[emote.ToLower()];
            }
        }
        else
        {
            Debug.LogWarning(emote + " emote for character " + name + " not found.");
        }
    }

    public void SelectCharacter()
    {
        if (gameObject.activeSelf && !selected)
        {
            spriteRenderer.color = Color.white;
            StartCoroutine(doHighlight(0.1f, true));
            selected = true;
        }
    }

    public void DeselectCharacter(Color fadeColor)
    {
        if (gameObject.activeSelf)
        {
            selected = false;
            spriteRenderer.color = fadeColor;
            if (HighlightedEffect != null)
            {
                HighlightedEffect.gameObject.SetActive(false);
            }
        }
    }
}
