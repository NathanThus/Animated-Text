using System;
using UnityEngine;

namespace TextAnimation.Common
{
    public class BaseAnimationObject : ScriptableObject
    {
        #region Serialized Fields
        [Header("== Identifier == ")]
        [SerializeField, Tooltip("The tag used for identification. Format should follow :XYZ123:")]
        private string _identifier;
        [SerializeField] private string _description;

        [Header("== Animation Data ==")]
        [SerializeField] private VectorPair _translationPair;

        [Header("== Colour Data == ")]
        [SerializeField]
        private Color _color = Color.white;
        [SerializeField] private Gradient _gradient;

        [Header("== Timing Data")]
        [SerializeField] private float _animationDelaySeconds = 0.05f;

        #endregion

        #region Properties

        public string Identifier => _identifier;
        public string Description => _description;

        public VectorPair TranslationPair => _translationPair;

        public Color Color => _color;
        public Gradient Gradient => _gradient;
        public float AnimationDelaySeconds => _animationDelaySeconds;


        #endregion

        #region Public

        /// <summary>
        /// The method to override, when creating your own implementation.
        /// </summary>
        /// <param name="mesh">The mesh that will be given to modify.</param>
        /// <returns>The modified mesh.</returns>
        public virtual Mesh DoEffect(Mesh mesh)
        {
            return mesh;
        }

        #endregion
    }
}