using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
public class BlockGeneratorScript : MonoBehaviour
{
    public float xGridSize, yGridSize;
    public float xSpacing, ySpacing;

    public void GenerateBlocks()
    {
        List<GameObject> blocks = new List<GameObject>();

        GameObject parentHolder = new GameObject();
        parentHolder.name = "Blocks";
        parentHolder.transform.position = Vector3.zero;

        for (int i = 0; i < xGridSize; i++)
        {
            for (int j = 0; j < yGridSize; j++)
            {
                Object blockPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Block.prefab", typeof(GameObject));
                GameObject block = PrefabUtility.InstantiatePrefab(blockPrefab, parentHolder.transform) as GameObject;
                block.transform.position = new Vector3(i * xSpacing, 0, j * ySpacing);
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