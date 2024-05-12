using System;
using UnityEngine;

namespace TextAnimation
{
    internal class BaseAnimationObject : ScriptableObject
    {
        #region Serialized Fields
        [Header("== Identifier == ")]
        [SerializeField] private string _identifier;
        [SerializeField] private string _description;
        [SerializeField] private AnimationType _animationType;

        [Header("== Animation Data ==")]
        [SerializeField] private VectorPair _translationPair;
        [SerializeField] private VectorPair _rotationPair;
        [SerializeField] private VectorPair _scalingPair;

        [Header("== Colour Data == ")]
        [SerializeField]
        private Color _color = Color.white
        ;
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
            return mesh;
        }

        #endregion

        #region Protected
        // These are all examples, will need to be sorted later.

        protected Mesh WaveAnimation(Mesh mesh)
        {
            Vector3[] newVertices = mesh.vertices;
            float currentTime = Time.time;
            for (var i = 0; i < newVertices.Length / 4; i++)
            {
                Vector3 Coordinates = Wave(currentTime + (i * TranslationPair.Maximum.x), TranslationPair.Maximum.y);
                for (int j = 0; j < 4; j++)
                {
                    newVertices[i*4+j] += Coordinates;
                }
            }

            mesh.vertices = newVertices;
            return mesh;
        }

        protected Mesh WobbleAnimation(Mesh mesh)
        {
            Vector3[] newVertices = mesh.vertices;
            for (var i = 0; i < mesh.vertices.Length; i++)
            {
                newVertices[i] += Wobble(Time.time + i,
                                         _translationPair.Maximum.x,
                                         _translationPair.Maximum.y);
            }
            mesh.vertices = newVertices;
            return mesh;
        }

        protected Mesh ColourByWhole(Mesh mesh)
        {
            Color[] colors = mesh.colors;
            Array.Fill<Color>(colors, _color);
            mesh.colors = colors;
            return mesh;
        }

        protected Mesh ApplyGradient(Mesh mesh)
        {
            Color[] newColors = mesh.colors;

            for (int i = 0; i < newColors.Length; i++)
            {
                newColors[i] = _gradient.Evaluate(Mathf.Repeat(Time.time + mesh.vertices[i].x * 0.001f, 1f));
            }

            mesh.colors = newColors;
            return mesh;
        }

        protected Vector3 Wobble(float time, float amplitudeX, float amplitudeY)
        {
            return new Vector3(MathF.Sin(time) * amplitudeX, Mathf.Cos(time) * amplitudeY, 0);
        }

        protected Vector3 Wave(float time, float amplitude)
        {
            return new Vector3(0, MathF.Sin(time) * amplitude, 0);
        }

        #endregion
    }
}