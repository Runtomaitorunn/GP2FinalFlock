using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Data.OleDb;

[CustomEditor(typeof(CompositeBehaviour))]

public class CompositeBehaviorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //setup
        CompositeBehaviour cb = (CompositeBehaviour)target;

        if (cb.behaviours == null || cb.behaviours.Length == 0)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox("No behaviors in array.", MessageType.Warning);
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Behaviors", GUILayout.MinWidth(60f), GUILayout.MaxWidth(290f));
            EditorGUILayout.LabelField("Weights", GUILayout.MinWidth(65f), GUILayout.MaxWidth(65f));
            EditorGUILayout.EndHorizontal();
            EditorGUI.BeginChangeCheck();

            for (int i = 0; i < cb.behaviours.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(i.ToString(), GUILayout.MinWidth(20f), GUILayout.MaxWidth(20f));
                cb.behaviours[i] = (FlockBehaviour)EditorGUILayout.ObjectField(cb.behaviours[i], typeof(FlockBehaviour), false, GUILayout.MinWidth(20f));
                cb.weights[i] = EditorGUILayout.FloatField(cb.weights[i], GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
                EditorGUILayout.EndHorizontal();
            }
            if (EditorGUI.EndChangeCheck())
            {
                //Undo.RecordObject(target, "Behaviors");
                EditorUtility.SetDirty(target);
                GUIUtility.ExitGUI();
            }
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Behavior"))
        {
            AddBehaviour(cb);
            GUIUtility.ExitGUI();
        }

        // Uncomment for button layout to be stacked

        /*EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();*/

        if (cb.behaviours != null && cb.behaviours.Length > 0)
        {
            if (GUILayout.Button("Remove Behavior"))
            {
                RemoveBehaviour(cb);
                GUIUtility.ExitGUI();
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    void AddBehaviour(CompositeBehaviour cb)
    {
        int oldCount = (cb.behaviours != null) ? cb.behaviours.Length : 0;
        FlockBehaviour[] newBehaviors = new FlockBehaviour[oldCount + 1];
        float[] newWeights = new float[oldCount + 1];

        for (int i = 0; i < oldCount; i++)
        {
            newBehaviors[i] = cb.behaviours[i];
            newWeights[i] = cb.weights[i];
        }
        newWeights[oldCount] = 1f;
        cb.behaviours = newBehaviors;
        cb.weights = newWeights;
    }

    void RemoveBehaviour(CompositeBehaviour cb)
    {
        int oldCount = cb.behaviours.Length;
        if (oldCount == 1)
        {
            cb.behaviours = null;
            cb.weights = null;
            return;
        }

        FlockBehaviour[] newBehaviors = new FlockBehaviour[oldCount - 1];
        float[] newWeights = new float[oldCount - 1];

        for (int i = 0; i < oldCount - 1; i++)
        {
            newBehaviors[i] = cb.behaviours[i];
            newWeights[i] = cb.weights[i];
        }
        cb.behaviours = newBehaviors;
        cb.weights = newWeights;
    }
}




//[CustomEditor(typeof(CompositeBehaviour))]
//public class CompositeBehaviourEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        // Set up
//        CompositeBehaviour cb = (CompositeBehaviour)target;

//        Rect r = EditorGUILayout.BeginHorizontal();
//        r.height = EditorGUIUtility.singleLineHeight;

//        // Check for behaviours
//        if (cb.behaviours == null || cb.behaviours.Length == 0)
//        {
//            EditorGUILayout.HelpBox("No bahaviours in array.", MessageType.Warning);
//            EditorGUILayout.EndHorizontal();
//            r = EditorGUILayout.BeginHorizontal();
//        }
//        else
//        {
//            r.x = 30f;
//            r.width = EditorGUIUtility.currentViewWidth - 95f;
//            EditorGUI.LabelField (r, "Behaviours");
//            r.x = EditorGUIUtility.currentViewWidth - 65f;
//            r.width = 60f;
//            EditorGUI.LabelField(r, "Weights");
//            r.y += EditorGUIUtility.singleLineHeight * 1.2f;

//            EditorGUI.BeginChangeCheck();

//            for (int i = 0; i < cb.behaviours.Length; i++)
//            {
//                r.x = 5f;
//                r.width = 20f;
//                EditorGUI.LabelField(r,i.ToString());
//                r.x = 30f;
//                r.width = EditorGUIUtility.currentViewWidth - 95f;
//                cb.behaviours[i] = (FlockBehaviour)EditorGUI.ObjectField(r, cb.behaviours[i], typeof(FlockBehaviour), false);
//                r.x = EditorGUIUtility.currentViewWidth - 65f;
//                r.width = 60f;
//                cb.weights[i] = EditorGUI.FloatField(r, cb.weights[i]);
//                r.y += EditorGUIUtility.singleLineHeight * 1.1f;            
//            }

//            if (EditorGUI.EndChangeCheck())
//            {
//                EditorUtility.SetDirty(cb);
//            }
//        }

//        EditorGUILayout.EndHorizontal();
//        r.x = 5f;
//        r.width = EditorGUIUtility.currentViewWidth - 10f;
//        r.y += EditorGUIUtility.singleLineHeight * 0.5f;
//        if (GUI.Button(r, "Add Behaviour"))
//        {
//            AddBahaviour(cb);
//            EditorUtility.SetDirty(cb);
//        }

//        r.y += EditorGUIUtility.singleLineHeight * 1.5f;
//        if (cb.behaviours != null && cb.behaviours.Length > 0)
//        {
//            if (GUI.Button(r, "Remove Behaviour"))
//            {
//                RemoveBahaviour(cb);
//                EditorUtility.SetDirty(cb);
//            }
//        }
//    }

//    void AddBahaviour(CompositeBehaviour cb)
//    {
//        int oldCount = (cb.behaviours != null) ? cb.behaviours.Length : 0;
//        FlockBehaviour[] newBehaviours = new FlockBehaviour[oldCount + 1];
//        float[] newWeights = new float[oldCount + 1];
//        for (int i = 0; i< oldCount; i++)
//        {
//            newBehaviours[i] = cb.behaviours[i];
//            newWeights[i] = cb.weights[i];
//        }
//        newWeights[oldCount] = 1f;
//        cb.behaviours = newBehaviours;
//        cb.weights = newWeights;
//    }

//    void RemoveBahaviour(CompositeBehaviour cb)
//    {
//        int oldCount = cb.behaviours.Length;
//        if (oldCount == 1)
//        {
//            cb.behaviours = null;
//            cb.weights = null;
//            return;
//        }
//        FlockBehaviour[] newBehaviours = new FlockBehaviour[oldCount - 1];
//        float[] newWeights = new float[oldCount - 1];
//        for (int i = 0; i < oldCount - 1; i++)
//        {
//            newBehaviours[i] = cb.behaviours[i];
//            newWeights[i] = cb.weights[i];
//        }
//        cb.behaviours = newBehaviours;
//        cb.weights = newWeights;
//    }
//}
