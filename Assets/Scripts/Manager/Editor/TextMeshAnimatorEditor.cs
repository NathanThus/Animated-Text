using UnityEngine;
using UnityEditor;
using TextAnimation;
using System.IO;
using System.Collections.Generic;
using TextAnimation.Common;
using System;

[CustomEditor(typeof(TextMeshAnimator))]
public class TextMeshAnimatorEditor : Editor
{
    TextMeshAnimator _animator;
    public override void OnInspectorGUI()
    {
        _animator = target as TextMeshAnimator;

        DrawDefaultInspector();

        if (GUILayout.Button("Refresh Animations"))
        {
            GetAllAnimations();
        }
    }

    private void GetAllAnimations()
    {
        var foundAssets = AssetDatabase.FindAssets("t:BaseAnimationObject");
        if (foundAssets.Length == 0) throw new FileNotFoundException("No Animations were found!");

        List<BaseAnimationObject> animations = new();
        foreach (var path in foundAssets)
        {
            var asset = AssetDatabase.LoadAssetAtPath<BaseAnimationObject>(AssetDatabase.GUIDToAssetPath(path));
            if (asset == null) throw new NullReferenceException(nameof(asset));
            animations.Add(asset);
        }

        _animator.UpdateAnimationList(animations);
    }
}