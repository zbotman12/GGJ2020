using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDirector : MonoBehaviour
{
    public Color spriteFadeColor;
    private List<DialogCharacter> characters = null;
    [SerializeField] CharacterMovementTypeAsset _characterMovement;

    private void Awake()
    {
        characters = new List<DialogCharacter>(FindObjectsOfType<DialogCharacter>());
    }

    public void ParseShowCharacterEvent(string[] args)
    {
        switch (args.Length)
        {
            case 2:
                ShowCharacter(args[0], args[1]);
                break;
            case 3:
                ShowCharacter(args[0], args[1], args[2]);
                break;
            case 4:
                ShowCharacter(args[0], args[1], args[2], args[3]);
                break;
            default:
                throw new System.ArgumentException($"Invalid number of args for Show Character Event.");
        }
    }

    public void ShowCharacter(string name, string sprite, string anim = "", string changeSpriteAfterAnim = "false")
    {
        var character = characters.Find(x => x.name.ToLower() == name.ToLower());
        if (character == null) Debug.LogWarning("No Such Character as " + name);
        character?.ShowCharacter(sprite, anim, changeSpriteAfterAnim == "true" ? true : false);
    }

    public void HideCharacter(string name, string sprite, string anim)
    {
        var character = characters.Find(x => x.name.ToLower() == name.ToLower());
        if (character == null) Debug.LogWarning("No Such Character as " + name);
        character?.HideCharacter(sprite, anim);
    }

    public void HighlightCharacter(string name)
    {
        foreach (DialogCharacter character in characters)
        {
            if (name != null && name.ToLower() == character.gameObject.name.ToLower())
            {
                character.SelectCharacter();
            }
            else
            {
                character.DeselectCharacter(spriteFadeColor);
            }
        }
    }
}
