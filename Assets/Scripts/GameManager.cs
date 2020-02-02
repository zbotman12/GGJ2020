using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject fairyPrefab;
    public GameObject fairySpawnPos1, fairySpawnPos2;
    public GameObject endScreen;

    public List<GameObject> faries = new List<GameObject>();

    public BlockGeneratorScript blockGenerator;

    public bool finished = false;
    public bool leftWinner;

    public bool TestingMode;

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

    public void StartGame()
    {
        InvokeRepeating("SpawnFairy", 3, 15);                 
        Time.timeScale = 1;
        blockGenerator.GenerateBlocks();
        StartCoroutine(WinChecker());
    }

    public IEnumerator WinChecker()
    {
        yield return new WaitForSeconds(3);
        leftWalls = new List<BlockBehavior>(blockGenerator.leftWalls);
        rightWalls = new List<BlockBehavior>(blockGenerator.rightWalls);
        yield return new WaitForSeconds(3);
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
        CameraShake.instance.StopScreenShake();
        Time.timeScale = 0;
        endScreen.SetActive(true);
    }

    public void SpawnFairy()
    {
        faries.Add(Instantiate(fairyPrefab, (Random.Range(0f, 1f) > .5f)? fairySpawnPos1.transform.position: fairySpawnPos2.transform.position, Quaternion.identity));
    }

    public void ResetGame()
    {
        finished = false;

        if (leftWalls.Count > 0)
            leftWalls.Clear();
        if (rightWalls.Count > 0)
            rightWalls.Clear();

        if (blockGenerator.leftWalls.Count > 0)
            blockGenerator.leftWalls.Clear();
        if (blockGenerator.rightWalls.Count > 0)
            blockGenerator.rightWalls.Clear();

        blockGenerator.DestroyChildren(blockGenerator.playerLeftPos.transform);
        blockGenerator.DestroyChildren(blockGenerator.playerRightPos.transform);

        foreach (GameObject fariy in faries)
            Destroy(fariy);       

        if (faries.Count > 0)
            faries.Clear();
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

