using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject fairyPrefab;
    public GameObject fairySpawnPos;

    public bool finished = false;
    public bool leftWinner;

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

    public void Start()
    {
        InvokeRepeating("SpawnFairy", 3, 15);
        StartCoroutine(WinChecker());
    }

    public IEnumerator WinChecker()
    {
        while (!finished)
        {
            for (int i = leftWalls.Count - 1; i >= 0; i--)
            {
                if (leftWalls[i].currHealth <= 0)
                    leftWalls.RemoveAt(i);
            }
            for (int i = rightWalls.Count - 1; i >= 0; i--)
            {
                if (rightWalls[i].currHealth <= 0)
                    rightWalls.RemoveAt(i);
            }
            if (leftWalls.Count == 0 || rightWalls.Count == 0)
                finished = true;
            yield return new WaitForSeconds(.2f);
        }

        // Game Over
        leftWinner = leftWalls.Count == 0;
        print(leftWinner);
    }

    public void SpawnFairy()
    {
        Instantiate(fairyPrefab, fairySpawnPos.transform.position, Quaternion.identity);
    }

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

