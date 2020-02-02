using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogEventListener), true)]
public class DialogEventListenerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SerializedProperty dEvent = serializedObject.FindProperty("_event");

        EditorGUILayout.PropertyField(dEvent);

        SerializedProperty sprop = serializedObject.FindProperty("_onRaisedZero");

        DialogEvent theEvent = (dEvent.objectReferenceValue as DialogEvent);
        if (theEvent != null)
        {
            switch ((dEvent.objectReferenceValue as DialogEvent).argumentCount)
            {
                case ArgumentCount.ZERO:
                    sprop = serializedObject.FindProperty("_onRaisedZero");
                    break;

                case ArgumentCount.ONE:
                    sprop = serializedObject.FindProperty("_onRaisedOne");
                    break;

                case ArgumentCount.TWO:
                    sprop = serializedObject.FindProperty("_onRaisedTwo");
                    break;

                case ArgumentCount.THREE:
                    sprop = serializedObject.FindProperty("_onRaisedThree");
                    break;

                case ArgumentCount.FOUR:
                    sprop = serializedObject.FindProperty("_onRaisedFour");
                    break;

                case ArgumentCount.VARIABLE:
                    sprop = serializedObject.FindProperty("_onRaisedVariable");
                    break;
            }

            EditorGUILayout.PropertyField(sprop);
        }

        serializedObject.ApplyModifiedProperties();

    }
}
