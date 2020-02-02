using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Material playerMaterial;
    public string playerName;

    public List<Material> playerSkins = new List<Material>();

    public List<PlayerSkin> skins = new List<PlayerSkin>();

    // Store Players Current Skin
    public PlayerSkin currentSkin;
    private int index = 0;

    public string buttonName;

    public void Start()
    {
        foreach (Transform child in transform.GetChild(1).transform)
            playerSkins.Add(child.GetComponent<MeshRenderer>().material);
    }

    public void LateUpdate()
    {
        if (Input.GetButtonDown(buttonName))
            SwapSkin();
    }

    public void SwapSkin()
    {
        if (index + 1 < skins.Count)
            index++;
        else
            index = 0;

        currentSkin = skins[index];

        foreach (Material skin in playerSkins)
            skin.mainTexture = currentSkin.skin;
    }
}
[System.Serializable]
public class PlayerSkin
{
    public Texture skin;
    public Color color;
}