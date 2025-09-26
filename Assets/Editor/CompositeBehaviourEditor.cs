using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CompositeBehaviour))]
public class CompositeBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Casting as a behaviour. target is the object being inspected
        CompositeBehaviour compositeBehaviour = (CompositeBehaviour)target;

        // Gives some space in the inspector 
        EditorGUILayout.Space();

        // Checking for if behaviours is null or if my array is empty
        if (compositeBehaviour.behaviours == null || compositeBehaviour.behaviours.Length == 0)
        {
            EditorGUILayout.HelpBox("No behaviours in array", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Behaviours", EditorStyles.boldLabel);

            EditorGUILayout.LabelField("Weight", GUILayout.Width(60));
            EditorGUILayout.EndHorizontal();

            // This checks between BeginChangeCheck() - EndChangeCheck() to see if anything changes 
            EditorGUI.BeginChangeCheck();

            for (int i = 0; i < compositeBehaviour.behaviours.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                // Displaying the array index (i) to the left side
                EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(10));

                // This shows the behaviour  
                compositeBehaviour.behaviours[i] = (FlockBehaviour)EditorGUILayout.ObjectField(
                    compositeBehaviour.behaviours[i],
                    typeof(FlockBehaviour), // Tell what type of object (flockbehaviour)  
                    false // The false if you wanted to use objects in scene I didn't so its false asset onlu
                );

                // Showing the weights 
                compositeBehaviour.weight[i] = EditorGUILayout.FloatField(compositeBehaviour.weight[i], GUILayout.Width(60));

                EditorGUILayout.EndHorizontal();
            }

            // if anything changed then 
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(compositeBehaviour, "Changed Behaviours");
                EditorUtility.SetDirty(compositeBehaviour);
            }
        }

        EditorGUILayout.Space();

        // Buttons for adding/removing behaviours
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Behaviour"))
        {
            // This helps record the old values so if you wanted to CTRL + Z or + Y it will work now
            Undo.RecordObject(compositeBehaviour, "Add Behaviour");
            //Adding my behaviour method
            AddBehavior(compositeBehaviour);
            // Setting it dirty so it saves changes 
            EditorUtility.SetDirty(compositeBehaviour);
        }

        // Only shows if my behaviours isn't empty     
        if (compositeBehaviour.behaviours != null && compositeBehaviour.behaviours.Length > 0)
        {
            // If we do then show button  
            if (GUILayout.Button("Remove Behaviour"))
            {
                Undo.RecordObject(compositeBehaviour, "Remove Behaviour");
                RemoveBehavior(compositeBehaviour);
                EditorUtility.SetDirty(compositeBehaviour);
            }
        }

        EditorGUILayout.EndHorizontal();
    }

    private void AddBehavior(CompositeBehaviour cb)
    {
        // if composite behaviour doesn't equal null then get composite behaviour count length else get 0.
        int oldCount = (cb.behaviours != null) ? cb.behaviours.Length : 0;

        FlockBehaviour[] newBehaviours = new FlockBehaviour[oldCount + 1];
        float[] newWeights = new float[oldCount + 1];

        for (int i = 0; i < oldCount; i++)
        {
            newBehaviours[i] = cb.behaviours[i];
            newWeights[i] = cb.weight[i];
        }

        newWeights[oldCount] = 1f;
        cb.behaviours = newBehaviours;
        cb.weight = newWeights;
    }

    private void RemoveBehavior(CompositeBehaviour cb)
    {
        int oldCount = cb.behaviours.Length;

        if (oldCount == 1)
        {
            cb.behaviours = null;
            cb.weight = null;
            return;
        }

        FlockBehaviour[] newBehaviours = new FlockBehaviour[oldCount - 1];
        float[] newWeights = new float[oldCount - 1];

        for (int i = 0; i < oldCount - 1; i++)
        {
            newBehaviours[i] = cb.behaviours[i];
            newWeights[i] = cb.weight[i];
        }
        cb.behaviours = newBehaviours;
        cb.weight = newWeights;
    }
}

