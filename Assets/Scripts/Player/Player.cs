using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Material playerMaterial;
    public string playerName;

    public List<MeshRenderer> meshFilters = new List<MeshRenderer>();

    public List<PlayerSkin> skins = new List<PlayerSkin>();

    // Store Players Current Skin
    public PlayerSkin currentSkin;
    private int index = 0;

    private Material playerMaterialInstance;

    private List<Fairy> dudes = new List<Fairy>();

    public Material PlayerMaterial => playerMaterialInstance;
    public string buttonName;

    Vector3 startPosition;
    Quaternion startRotation;

    public void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        playerMaterialInstance = new Material(playerMaterial);
        foreach (MeshRenderer mf in meshFilters)
            mf.material = playerMaterialInstance;
    }

    public void RegisterFairy(Fairy f)
    {
        dudes.Add(f);
    }

    public void Reset()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
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

        playerMaterialInstance.mainTexture = currentSkin.skin;

        foreach(Fairy dude in dudes)
        {
            dude.SetMaterial(playerMaterialInstance);
        }
    }
}
[System.Serializable]
public class PlayerSkin
{
    public Texture skin;
    public Color color;
}