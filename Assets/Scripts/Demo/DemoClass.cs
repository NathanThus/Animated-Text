using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

namespace TextAnimation.Demo
{
    public class DemoClass : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private TextMeshAnimator _textAnimator;
        [Header("Text Fields")]
        [SerializeField] private List<TMP_Text> _textFields = new();
        [SerializeField] private TMP_Text _targetMesh;

        private CancellationTokenSource _tokenSource = new();

        private void OnDestroy()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }

        public void Prepare()
        {
            foreach (var item in _textFields)
            {
                if (!_textAnimator.CreateAnimationPair(item))
                {
                    Debug.Log("Did not find a valid animation in" + item.gameObject.name);
                }
            }
        }

        public void StartAnimations()
        {
            _textAnimator.EnableAnimations(_tokenSource.Token);
        }

        public void StopAnimations()
        {
            _textAnimator.DisableAnimations();
        }
        
        public void AddToList()
        {
            if(_textAnimator.CreateAnimationPair(_targetMesh))
            {
                _textFields.Add(_targetMesh);
            }
            else
            {
                Debug.Log("Remember to add an animation to the text field!");
            }
        }

        public void RemoveFromList()
        {
            _textAnimator.KillAnimation(_targetMesh);
            _textFields.Remove(_targetMesh);
        }
    }
}
