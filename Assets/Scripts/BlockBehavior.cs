using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
{    
    public float currHealth;
    public int startHealth, maxHealth;

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
    }

    public void damage(int mod)
    {
        CameraShake.instance.ShakeCamera();
        addHealth(-1 * mod);
    }
    public void damage(float mod)
    {
        Debug.Log("DamageHappened");
        CameraShake.instance.ShakeCamera();
        addHealth(-1 * mod);
    }
}
