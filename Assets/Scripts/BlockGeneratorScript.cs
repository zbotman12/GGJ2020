using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CanEditMultipleObjects]
#endif
public class BlockGeneratorScript : MonoBehaviour
{
    public float xGridSize, yGridSize;
    public float xSpacing, ySpacing;
    public GameObject playerLeftPos, playerRightPos, blockPrefab;
    public List<BlockBehavior> leftWalls = new List<BlockBehavior>(), rightWalls = new List<BlockBehavior>();

    public void GenerateBlocks()
    {
        GenerateTheBlocks(true, playerLeftPos);
        GenerateTheBlocks(false, playerRightPos);
    }
    public void DestroyChildren(Transform root)
    {
        int childCount = root.childCount;
        for (int i = 0; i < childCount; i++)
        {
            GameObject.Destroy(root.GetChild(0).gameObject);
        }
    }
    public void GenerateTheBlocks(bool leftSide, GameObject parentHolder)
    {        
        for (int i = 0; i < xGridSize; i++)
        {
            for (int j = 0; j < yGridSize; j++)
            {
                GameObject block = Instantiate(blockPrefab, parentHolder.transform);
                block.transform.position = new Vector3(i * xSpacing + (leftSide? playerLeftPos.transform.position.x: playerRightPos.transform.position.x), 1, j * ySpacing + (leftSide ? playerLeftPos.transform.position.x+7 : -playerRightPos.transform.position.x+5));
                if (leftSide)
                    leftWalls.Add(block.GetComponent<BlockBehavior>());
                else
                    rightWalls.Add(block.GetComponent<BlockBehavior>());
            }
        }
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(BlockGeneratorScript))]
public class BlockManagerEditor: Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BlockGeneratorScript script = target as BlockGeneratorScript;
        if (GUILayout.Button("Generate Blocks"))
        {
            script.GenerateBlocks();
        }
    }
}
#endif