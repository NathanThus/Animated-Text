using UnityEngine;
using UnityEditor;
using TextAnimation.Demo;

[CustomEditor(typeof(DemoClass))]
public class DemoEditor : Editor
{
    private DemoClass demoClass;

    private SerializedProperty _textAnimator;
    private SerializedProperty _textFields;
    private SerializedProperty _targetMesh;

    private void OnEnable()
    {
        _textAnimator = serializedObject.FindProperty(nameof(_textAnimator));
        _textFields = serializedObject.FindProperty(nameof(_textFields));
        _targetMesh = serializedObject.FindProperty(nameof(_targetMesh));
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        demoClass = target as DemoClass;

        EditorGUILayout.PropertyField(_textAnimator, new GUIContent("Text Animator"));
        EditorGUILayout.PropertyField(_textFields, new GUIContent("Text Animator"));

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Prepare"))
        {
            demoClass.Prepare();
        }
        if (GUILayout.Button("Start"))
        {
            demoClass.StartAnimations();
        }
        if (GUILayout.Button("Stop"))
        {
            demoClass.StopAnimations();
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_targetMesh, new GUIContent("Target"));
        EditorGUILayout.LabelField(new GUIContent("Modify List"));

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add"))
        {
            demoClass.AddToList();
        }
        if (GUILayout.Button("Remove"))
        {
            demoClass.RemoveFromList();
        }
        GUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
    }
}