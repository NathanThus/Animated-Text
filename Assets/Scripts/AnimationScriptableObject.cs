using System;
using UnityEngine;

namespace TextAnimation
{

    [CreateAssetMenu(fileName = nameof(AnimationScriptableObject), menuName = "TextAnimation/" + nameof(AnimationScriptableObject), order = 1)]
    internal class AnimationScriptableObject : ScriptableObject
    {
        #region Serialized Fields
        [Header("== Identifier == ")]
        [SerializeField] private string _identifier;
        [SerializeField] private string _description;

        [Header("== Animation Data ==")]
        [SerializeField] private VectorPair _translationPair;
        [SerializeField] private VectorPair _rotationPair;
        [SerializeField] private VectorPair _scalingPair;

        [Header("== Colour Data == ")]
        [SerializeField] private Color _color;
        [SerializeField] private Gradient _gradient;

        #endregion

        #region Properties

        internal string Identifier => _identifier;
        internal string Description => _description;

        internal VectorPair TranslationPair => _translationPair;
        internal VectorPair RotationPair => _rotationPair;
        internal VectorPair ScalingPair => _scalingPair;

        internal Color Color => _color;
        internal Gradient Gradient => _gradient;


        #endregion

        #region Internal
        /// <summary>
        /// The method to override, when creating your own implementation.
        /// </summary>
        /// <param name="mesh">The mesh that will be given to modify.</param>
        /// <returns>The modified mesh.</returns>
        internal virtual Mesh DoEffect(Mesh mesh)
        {
            return ApplyGradient(ExampleMethod(mesh));
        }

        #endregion

        // These are all examples, will need to be sorted later.

        #region Private

        private Mesh ExampleMethod(Mesh mesh)
        {
            Vector3[] newVertices = mesh.vertices;
            for (var i = 0; i < mesh.vertices.Length; i++)
            {
                newVertices[i] += Wobble(Time.time + i, _translationPair.Maximum.x, _translationPair.Maximum.y);
            }
            mesh.vertices = newVertices;
            return mesh;
        }

        private Mesh ColourByWhole(Mesh mesh)
        {
            Color[] colors = mesh.colors;
            Array.Fill<Color>(colors, _color);
            mesh.colors = colors;
            return mesh;
        }

        private Mesh ApplyGradient(Mesh mesh)
        {
            Color[] newColors = mesh.colors;

            for (int i = 0; i < newColors.Length; i++)
            {
                newColors[i] = _gradient.Evaluate(Mathf.Repeat(Time.time + mesh.vertices[i].x * 0.001f, 1f));
            }

            mesh.colors = newColors;
            return mesh;
        }

        private Vector3 Wobble(float time, float amplitudeX, float amplitudeY)
        {
            return new Vector3(MathF.Sin(time * amplitudeX), Mathf.Cos(time * amplitudeY));
        }


        #endregion
    }
}