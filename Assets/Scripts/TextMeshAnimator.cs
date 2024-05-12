using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace TextAnimation
{
    public class TextMeshAnimator : MonoBehaviour
    {
        #region Serialized Fields
        [Header("Target")]
        [SerializeField] private TMP_Text _textMesh;
        [SerializeField] private List<BaseAnimationObject> _availableAnimations;
        [SerializeField] private List<BaseAnimationObject> _selectedAnimations;

        #endregion

        #region Fields

        protected Mesh _mesh;
        protected List<int> _wordLengths = new();
        protected List<int> _wordIndexes = new() { 0 };

        const string RegexPattern = ":[a-zA-Z0-9]*:";
        const RegexOptions DesiredRegexOptions = RegexOptions.Multiline;

        #endregion

        #region Unity Methods
        // Start is called before the first frame update
        private void Start()
        {
            PrepareWords();
        }

        private void Update()
        {
            UpdateText();
        }

        #endregion

        #region Public

        /// <summary>
        /// Updates the text with the desired properties.
        /// </summary>
        public void UpdateText()
        {
            GatherData();

            foreach (BaseAnimationObject anim in _selectedAnimations)
            {
                _mesh = anim.DoEffect(_mesh);
            }

            SetMeshProperties();
        }

        /// <summary>
        /// Enables the Monobehaviour.
        /// </summary>
        public void Enable()
        {
            enabled = true;
        }

        /// <summary>
        /// Disables the Monobehaviour.
        /// </summary>
        public void Disable()
        {
            enabled = false;
        }

        #endregion

        #region Private

        private void GatherData()
        {
            _textMesh.ForceMeshUpdate();
            _mesh = _textMesh.mesh;
        }

        /// <summary>
        /// Prepares the system by separating the words by spacing.
        /// </summary>
        private void PrepareWords()
        {
            if (!PerformEffectDetection()) return;
            string text = _textMesh.text;

            for (int i = text.IndexOf(' '); i > -1; i = text.IndexOf(' ', i + 1))
            {
                _wordLengths.Add(i - _wordIndexes[_wordIndexes.Count - 1]);
                _wordIndexes.Add(i + 1);
            }
            _wordLengths.Add(text.Length - _wordIndexes[_wordIndexes.Count - 1]);
        }

        public bool PerformEffectDetection()
        {
            string text = _textMesh.text;
            MatchCollection matches = Regex.Matches(text, RegexPattern, DesiredRegexOptions);

            if (matches.Count == 0) // If there are no effects, then who cares.
            {
                Disable();
                return false;
            }

            foreach (Match match in matches)
            {
                BaseAnimationObject animation = _availableAnimations.Find(anim => match.Value == anim.Identifier);

                if (animation != null)
                {
                    _selectedAnimations.Add(animation);
                }

                text = text.Remove(text.IndexOf(match.Value), match.Length);
            }

            _textMesh.text = text;
            if (_selectedAnimations.Count == 0)
            {
                Disable();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Call this after every modification of the verticies.
        /// </summary>
        private void SetMeshProperties()
        {
            _textMesh.canvasRenderer.SetMesh(_mesh);
        }

        #endregion
    }
}