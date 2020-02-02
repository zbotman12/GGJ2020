using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
{    
    public float currHealth;
    public int startHealth, maxHealth;

    private MeshFilter mesh;
    public Mesh FullHealth, Damaged, Broken;

    public void Start()
    {
        mesh = GetComponent<MeshFilter>();        
    }

    BlockBehavior()
    {
        currHealth = startHealth = maxHealth = 100;
    }

    public BlockBehavior(int all)
    {
        currHealth = startHealth = maxHealth = all;
    }

    BlockBehavior(int start, int max)
    {
        currHealth = startHealth = start;
        maxHealth = max;
    }

    public void addHealth(int mod)
    {
        currHealth += mod;
        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        } else if (currHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (currHealth > 80)
            mesh.mesh = FullHealth;
        else if (currHealth <= 80 && currHealth >= 25)
            mesh.mesh = Damaged;
        else
            mesh.mesh = Broken;
    }
    public void addHealth(float mod)
    {
        currHealth += mod;
        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
        else if (currHealth <= 0)
        {
            // This will break the GameManager with a nullrefexecption
            //Destroy(gameObject);            
            gameObject.SetActive(false);
        }

        if (currHealth > 80)
            mesh.mesh = FullHealth;
        else if (currHealth < 75 && currHealth >= 25)
            mesh.mesh = Damaged;
        else
            mesh.mesh = Broken;
    }

    public void damage(int mod)
    {
        CameraShake.instance.ShakeCamera();
        addHealth(-1 * mod);
    }
    public void damage(int mod, float shakeAmt)
    {
        CameraShake.instance.ShakeCamera(shakeAmt);
        addHealth(-1 * mod);
    }
    public void damage(float mod)
    {
        CameraShake.instance.ShakeCamera();
        addHealth(-1 * mod);
    }
}
