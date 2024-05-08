using System;
using UnityEngine;

namespace TextAnimation
{

    [CreateAssetMenu(fileName = "AnimationScriptableObject", menuName = "TextAnimation/Animation ScriptableObject", order = 1)]
    internal class AnimationScriptableObject : ScriptableObject
    {
        #region Serialized Fields
        [Header("== Identifier == ")]
        public string _identifier;
        [Header("== Settings ==")]
        [SerializeField] private bool _singleUse;

        [Header("== Animation Data ==")]
        [SerializeField] private VectorPair _translationPair;
        [SerializeField] private VectorPair _rotationPair;
        [SerializeField] private VectorPair _scalingPair;

        [Header("== Colour Data == ")]
        [SerializeField] private Color _color;
        [SerializeField] private Gradient _gradient;

        #endregion

        #region Properties

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
            return ColourByWhole(ExampleMethod(mesh));
        }

        #endregion

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
            Array.Fill<Color>(colors,_color);
            mesh.colors = colors;
            return mesh;
        }

        private Vector3 Wobble(float time, float amplitudeX, float amplitudeY)
        {
            return new Vector3(MathF.Sin(time * amplitudeX), Mathf.Cos(time * amplitudeY));
        }


        #endregion
    }
}