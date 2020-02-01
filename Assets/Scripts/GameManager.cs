using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Tooltip("The players walls list that will store all of the data for the walls")]
    public List<BlockBehavior> leftWalls = new List<BlockBehavior>(), rightWalls = new List<BlockBehavior>();

    #region SingletonDeclration
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
            Destroy(gameObject);        
    }
    #endregion

    /// <summary>
    /// API for the Faries to find the weakest wall to repair
    /// </summary>
    /// <returns>The block behavior associcated with the block</returns>
    public BlockBehavior FindWeakestWall(bool leftSide)
    {
        BlockBehavior temp = null;
        float tempHealth = 101;
        foreach (BlockBehavior block in leftSide ? leftWalls : rightWalls)
        {
            try
            {
                // Dont return broken/destroyed blocks
                if (tempHealth > block.currHealth && block.currHealth > 0)
                {
                    temp = block;
                    tempHealth = block.currHealth;
                }
            }
            catch (System.Exception){}

        }

        return temp;
    }
}

