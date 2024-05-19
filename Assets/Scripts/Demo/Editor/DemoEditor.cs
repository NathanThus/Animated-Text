using UnityEngine;
using UnityEditor;
using TextAnimation.Demo;

[CustomEditor(typeof(DemoClass))]
public class DemoEditor : Editor
{
    private DemoClass demoClass;
    public override void OnInspectorGUI()
    {
        demoClass = target as DemoClass;
        DrawDefaultInspector();

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

    }
}