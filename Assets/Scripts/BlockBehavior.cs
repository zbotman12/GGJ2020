using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
{
    public static Camera gameCamera;
    private int currHealth;
    public int startHealth, maxHealth;

    BlockBehavior()
    {
        currHealth = startHealth = maxHealth = 100;
    }

    BlockBehavior(int all)
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

    public void damage(int mod)
    {
        addHealth(-1 * mod);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
