using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject fairyPrefab;

    private GameObject CharacterParent;

    private GameObject endScreen;
    private GameObject mainMenu;


    public List<GameObject> faries = new List<GameObject>();

    public bool finished = false;
    public bool leftWinner;


    private Coroutine spawnFairyRoutine;
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
            endScreen = GameObject.FindObjectOfType<EndScreen>().gameObject;
            endScreen.SetActive(false);
            mainMenu = GameObject.FindObjectOfType<MainMenu>().gameObject;
            mainMenu.SetActive(true);
            CharacterParent = GameObject.FindObjectOfType<AudioChorusFilter>().gameObject;
        }
        else if (instance != this)
            Destroy(gameObject);        
    }
    #endregion

    public void StartGame()
    {
        if(CharacterParent == null) CharacterParent = GameObject.FindObjectOfType<AudioChorusFilter>().gameObject;
        if(endScreen == null) endScreen = GameObject.FindObjectOfType<EndScreen>().gameObject;
        if(mainMenu == null) mainMenu = GameObject.FindObjectOfType<MainMenu>().gameObject;
        endScreen.SetActive(false);
        spawnFairyRoutine = StartCoroutine(SpawnFairies());
        FindObjectOfType<BlockGeneratorScript>().GenerateBlocks();
        StartCoroutine(WinChecker());
        FindObjectOfType<BallSpawner>().SpawnBall(0);
        foreach(BasicMovement bm in FindObjectsOfType<BasicMovement>())
        {
            bm.enabled = true;
        }

        foreach (GrabBall bm in FindObjectsOfType<GrabBall>())
        {
            bm.enabled = true;
        }

        FindObjectOfType<BallSpawner>().SpawnBall(0);
    }

    public IEnumerator SpawnFairies()
    {
        yield return new WaitForSeconds(3);
        while (true)
        {
            SpawnFairy();
            yield return new WaitForSeconds(15);
        }

    }

    public IEnumerator WinChecker()
    {
        yield return new WaitForSeconds(3);
        leftWalls = new List<BlockBehavior>(FindObjectOfType<BlockGeneratorScript>().leftWalls);
        rightWalls = new List<BlockBehavior>(FindObjectOfType<BlockGeneratorScript>().rightWalls);
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
        foreach (BasicMovement bm in FindObjectsOfType<BasicMovement>())
        {
            bm.enabled = false;
        }

        foreach (GrabBall bm in FindObjectsOfType<GrabBall>())
        {
            bm.enabled = false;
        }
        endScreen.SetActive(true);
        EventSystem.current.SetSelectedGameObject(endScreen.GetComponentInChildren<UnityEngine.UI.Button>().gameObject);
    }

    public void SpawnFairy()
    {
        bool odds = Random.Range(0f, 1f) > .5f;
        faries.Add(Instantiate(fairyPrefab, (odds) ? GameObject.Find("FairySpawnPos1").transform.position : GameObject.Find("FairySpawnPos2").transform.position, Quaternion.identity));
        faries[faries.Count - 1].GetComponent<Fairy>().SetDestination((odds )? new Vector3(0,1,2f) : new Vector3(0, 1, -2f));
    }

    public void ResetGame()
    {
        FindObjectOfType<BallSpawner>().StopAllCoroutines();
        finished = false;
        endScreen.SetActive(false);
        mainMenu.SetActive(true);
        if (leftWalls.Count > 0)
            leftWalls.Clear();
        if (rightWalls.Count > 0)
            rightWalls.Clear();

        if (FindObjectOfType<BlockGeneratorScript>().leftWalls.Count > 0)
            FindObjectOfType<BlockGeneratorScript>().leftWalls.Clear();
        if (FindObjectOfType<BlockGeneratorScript>().rightWalls.Count > 0)
            FindObjectOfType<BlockGeneratorScript>().rightWalls.Clear();

        FindObjectOfType<BlockGeneratorScript>().DestroyChildren(FindObjectOfType<BlockGeneratorScript>().playerLeftPos.transform);
        FindObjectOfType<BlockGeneratorScript>().DestroyChildren(FindObjectOfType<BlockGeneratorScript>().playerRightPos.transform);
        CharacterParent.SetActive(true);
        CharacterParent.GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
        CharacterParent.GetComponentsInChildren<SpriteRenderer>()[2].enabled = false;

        StopCoroutine(spawnFairyRoutine);

        foreach (GameObject fariy in faries)
            Destroy(fariy);       

        if (faries.Count > 0)
            faries.Clear();

        foreach(Player p in FindObjectsOfType<Player>())
        {
            p.Reset();
        }

        System.Array.ForEach(FindObjectsOfType<BlockBehavior>(), x => Destroy(x.gameObject));
        System.Array.ForEach(FindObjectsOfType<PeeShooterCollision>(), x => Destroy(x.gameObject));
        System.Array.ForEach(FindObjectsOfType<Ball>(), x => Destroy(x.gameObject));
        System.Array.ForEach(FindObjectsOfType<Explosion>(), x => Destroy(x.gameObject));

        //endScreen.GetComponent<LoadNextScene>().LoadScene();
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

