using System;
using TMPro;
using UnityEngine;

namespace TextAnimation
{
    [CreateAssetMenu(fileName = "TranslationAnimation", menuName = "TextAnimation/Movement/TranslationAnimation", order = 1)]
    public class TranslationAnimationValues : EffectScriptableObject
    {
        [Tooltip("Amplitude for the wobble.")]
        public float Vertical;
        [Tooltip("Amplitude for the wobble.")]
        public float Horizontal;

        public override Mesh DoEffect(Mesh mesh)
        {
            Vector3[] newVertices = mesh.vertices;
            for (var i = 0; i < mesh.vertices.Length; i++)
            {
                newVertices[i] += Wobble(Time.time + i);
            }
            mesh.vertices = newVertices;
            return mesh;
        }

        private Vector3 Wobble(float time)
        {
            return new Vector3(MathF.Sin(time * Horizontal), Mathf.Cos(time * Vertical));
        }
    }
}