using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CompositeBehaviour)]
public class CompositeBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CompositeBehaviour cb = (CompositeBehaviour)target;

        Rect r = EditorGUILayout.BeginHorizontal();
        r.height = EditorGUIUtility.singleLineHeight;

        if (cb.behaviours == null || cb.behaviours.Length == 0)
        {
            EditorGUILayout.HelpBox("No behaviours in array ",MessageType.Warning);
            EditorGUILayout.EndHorizontal();
            r = EditorGUILayout.BeginHorizontal();

        }
        else
        {

        }
    }
}
