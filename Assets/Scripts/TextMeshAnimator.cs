using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TextAnimation
{
    public class TextMeshAnimator : MonoBehaviour
    {
        #region Serialized Fields
        [Header("Target")]
        [SerializeField] private TMP_Text _textMesh;

        [Header("Wobble")]
        [SerializeField] protected float _horizontalWobble = 3.3f;
        [SerializeField] protected float _verticalWobble = 0.8f;

        [SerializeField] private bool _moveVerticePerWord;
        [SerializeField] private bool _moveVerticePerLetter;
        [SerializeField] private bool _moveEntireMesh;

        [Header("Colour")]
        [SerializeField] protected Gradient _gradient;
        [SerializeField] private bool _useColour;

        [SerializeField] private AnimationScriptableObject _animationObj;

        #endregion

        #region Fields

        protected Mesh _mesh;
        protected Vector3[] _verticies;
        protected List<int> _wordLengths = new();
        protected List<int> _wordIndexes = new() { 0 };
        protected Color[] _colors;

        const int VerticeCount = 4;

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
            
            if(_moveEntireMesh)
            {
                _mesh = _animationObj.DoEffect(_mesh);
            }
            // if (_moveVerticePerWord) VerticePerWord();
            // if (_moveVerticePerLetter) VerticePerLetter();

            // if (_colors != null)
            // {
            //     if (_useColour) ColorPerWord();
            //     else Array.Fill<Color>(_colors, Color.white);
            // }

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
                if (match.Value.Contains(":wave:"))
                {
                    _moveEntireMesh = true;
                }

                text = text.Remove(match.Index, match.Length);
            }

            _textMesh.text = text;

            return true;
        }

        /// <summary>
        /// Changes the vertice of letters individually.
        /// </summary>
        private void VerticePerLetter()
        {
            for (var i = 0; i < _textMesh.textInfo.characterCount; i++)
            {
                TMP_CharacterInfo characterInfo = _textMesh.textInfo.characterInfo[i];
                int index = characterInfo.vertexIndex;

                Vector3 offset = Wobble(Time.time + i);
                for (var j = 0; j < VerticeCount; j++)
                {
                    _verticies[index + j] += offset;
                }
            }

        }

        /// <summary>
        /// Changes the verticies of the words.
        /// </summary>
        private void VerticePerWord()
        {
            for (int w = 0; w < _wordIndexes.Count; w++)
            {
                int word = _wordIndexes[w];
                Vector3 offset = Wobble(Time.time + w);

                for (var i = 0; i < _wordLengths[w]; i++)
                {
                    TMP_CharacterInfo characterInfo = _textMesh.textInfo.characterInfo[word + i];

                    int vertexIndex = characterInfo.vertexIndex;

                    for (int j = 0; j < VerticeCount; j++)
                    {
                        _verticies[vertexIndex + j] += offset;
                    }
                }
            }

        }

        /// <summary>
        /// Colours the words individually.
        /// </summary>s
        private void ColorPerWord()
        {
            _colors = _mesh.colors;

            for (int w = 0; w < _wordIndexes.Count; w++)
            {
                int word = _wordIndexes[w];

                for (var i = 0; i < _wordLengths[w]; i++)
                {
                    TMP_CharacterInfo characterInfo = _textMesh.textInfo.characterInfo[word + i];
                    int index = characterInfo.vertexIndex;

                    for (int j = 0; j < 4; j++)
                    {
                        _colors[index + j] = _gradient.Evaluate(Mathf.Repeat(Time.time + _verticies[index + j].x * 0.001f, 1f));
                    }
                }
            }
        }

        private void VerticePerMesh()
        {
            for (var i = 0; i < _verticies.Length; i++)
            {
                Vector3 offset = Wobble(Time.time + i);
                _verticies[i] += offset;
            }
        }

        /// <summary>
        /// Call this after every modification of the verticies.
        /// </summary>
        private void SetMeshProperties()
        {
            _mesh.colors = _colors;
            // _mesh.vertices = _verticies; // REWIND THIS IF IT FUCKS UP
            _textMesh.canvasRenderer.SetMesh(_mesh);
        }

        private Vector2 Wobble(float time)
        {
            return new Vector2(MathF.Sin(time * _horizontalWobble), Mathf.Cos(time * _verticalWobble));
        }
        #endregion
    }
}