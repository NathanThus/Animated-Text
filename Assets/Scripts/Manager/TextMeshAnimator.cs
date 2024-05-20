using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TextAnimation.Common;
using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace TextAnimation
{
    public class TextMeshAnimator : MonoBehaviour
    {
        #region Nested Entities

        private struct TextAnimationPair
        {
            private TMP_Text _textMesh;
            private List<BaseAnimationObject> _animations;
            private Mesh _mesh;
            private float _animationDelaySeconds;

            public readonly TMP_Text TextMesh => _textMesh;
            public readonly List<BaseAnimationObject> Animations => _animations;
            public readonly Mesh Mesh => _mesh;

            public readonly float DelaySeconds => _animationDelaySeconds;

            public TextAnimationPair(TMP_Text textMesh, List<BaseAnimationObject> animations)
            {
                _textMesh = textMesh;
                _animations = animations;
                _mesh = _textMesh.mesh;

                BaseAnimationObject animationDelay = _animations[0];
                foreach (BaseAnimationObject anim in _animations)
                {
                    if (anim.AnimationDelaySeconds > animationDelay.AnimationDelaySeconds)
                    {
                        animationDelay = anim;
                    }
                }

                if (animationDelay.AnimationDelaySeconds == 0f)
                {
                    throw new ArgumentOutOfRangeException(nameof(animationDelay),
                                "Please remember to set a delay, both for performance and to avoid eye strain!");
                }
                _animationDelaySeconds = animationDelay.AnimationDelaySeconds;
            }

            public void Prepare()
            {
                _textMesh.ForceMeshUpdate();
                _mesh = _textMesh.mesh;
            }

            /// <summary>
            /// Call this after every modification of the verticies.
            /// </summary>
            public readonly void SetMeshProperties(Mesh mesh)
            {
                _textMesh.canvasRenderer.SetMesh(mesh);
            }

            /// <summary>
            /// Prepares the struct for garbage collection.
            /// </summary>
            public void Kill()
            {
                _textMesh = null;
                _mesh = null;
                _animations.Clear();
            }
        }

        #endregion

        #region Serialized Fields
        [Header("Animations")]
        [SerializeField] private List<BaseAnimationObject> _availableAnimations;        

        #endregion

        #region Fields

        protected Mesh _mesh;
        protected List<int> _wordLengths = new();
        protected List<int> _wordIndexes = new() { 0 };

        private List<TextAnimationPair> _textAnimationPairs = new();
        private bool _doAnimation = true;

        const string RegexPattern = ":[a-zA-Z0-9]*:";
        const RegexOptions DesiredRegexOptions = RegexOptions.Multiline;

        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            foreach (var item in _textAnimationPairs)
            {
                item.Kill();
            }
        }

        #endregion 

        #region Public

        /// <summary>
        /// 
        /// </summary>
        public async void UpdateText(CancellationToken token)
        {
            float animationDelay = float.MaxValue;

            foreach (var textAnimation in _textAnimationPairs)
            {
                if (animationDelay > textAnimation.DelaySeconds)
                {
                    animationDelay = textAnimation.DelaySeconds;
                }
            }

            while (_doAnimation)
            {
                foreach (var animationPair in _textAnimationPairs)
                {
                    animationPair.Prepare();
                    Mesh animationMesh = animationPair.Mesh;
                    foreach (var animation in animationPair.Animations)
                    {
                        animationMesh = animation.DoEffect(animationMesh);
                    }
                    animationPair.SetMeshProperties(animationMesh);
                }
                await UniTask.WaitForSeconds(animationDelay, cancellationToken: token);
            }

        }

        /// <summary>
        /// Creates a new AnimationPair. Creates and prepares an animation pair if successful.
        /// </summary>
        /// <param name="TMPElement">The TextMeshPro Text element to parse into the pair</param>
        /// <returns>If any valid animations were found.</returns>
        public bool CreateAnimationPair(TMP_Text TMPElement)
        {
            if (DoesElementExist(TMPElement)) return true;

            List<BaseAnimationObject> animations = PerformEffectDetection(TMPElement);
            if (animations == null ||animations.Count == 0) return false; // No animations detected

            _textAnimationPairs.Add(new TextAnimationPair(TMPElement, animations));
            return true;
        }

        /// <summary>
        /// Disables animations, kills all animation pairs, then clears the list.
        /// </summary>
        public void KillAllAnimations()
        {
            DisableAnimations();
            foreach (var item in _textAnimationPairs)
            {
                item.Kill();
            }
            _textAnimationPairs.Clear();
        }

        /// <summary>
        /// Kills a specific element, and readies it for garbage collection.
        /// </summary>
        /// <param name="TMPElement">The element to search for.</param>
        public void KillAnimation(TMP_Text TMPElement)
        {
            var animation = _textAnimationPairs.Find(anim => TMPElement == anim.TextMesh);

            if(animation.Equals(default(TextAnimationPair))) return;

            _textAnimationPairs.Remove(animation);
            animation.Kill();
        }

        /// <summary>
        /// Enables all animations.
        /// </summary>
        public void EnableAnimations(CancellationToken token)
        {
            _doAnimation = true;
            UpdateText(token);
        }

        /// <summary>
        /// Enables all animations (Stops the loop).
        /// </summary>
        public void DisableAnimations()
        {
            _doAnimation = false;
        }

        /// <summary>
        /// Updates the animation list with a new animation. 
        /// Should not be called, instead only called by the TextMeshAnimatorEditor.
        /// </summary>
        /// <param name="animationList">The new animation list</param>
        /// <exception cref="ArgumentNullException">If the animation list is null.</exception>
        public void UpdateAnimationList(List<BaseAnimationObject> animationList)
        {
            if (animationList == null) throw new ArgumentNullException(nameof(animationList));
            _availableAnimations.Clear();

            foreach (var item in animationList)
            {
                _availableAnimations.Add(item);
            }
        }

        #endregion

        #region Private

        public List<BaseAnimationObject> PerformEffectDetection(TMP_Text TMPElement)
        {
            string text = TMPElement.text;
            MatchCollection matches = Regex.Matches(text, RegexPattern, DesiredRegexOptions);

            if (matches.Count == 0) // If there are no effects, then who cares.
            {
                return null;
            }

            List<BaseAnimationObject> animations = new();

            foreach (Match match in matches)
            {
                BaseAnimationObject animation = _availableAnimations.Find(anim => match.Value == anim.Identifier);

                if (animation != null)
                {
                    animations.Add(animation);
                }

                text = text.Remove(text.IndexOf(match.Value), match.Length);
            }

            TMPElement.text = text;
            if (animations.Count == 0)
            {
                return null;
            }

            return animations;
        }

        private bool DoesElementExist(TMP_Text TMPElement)
        {
            foreach (var textMesh in _textAnimationPairs)
            {
                if (textMesh.TextMesh == TMPElement)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}