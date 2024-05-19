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
    }
}
