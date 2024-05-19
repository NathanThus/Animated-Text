using System;
using System.Threading;
using TMPro;
using UnityEngine;

namespace TextAnimation.Common
{

    [CreateAssetMenu(fileName = nameof(FlashColourAnimation), menuName = "TextAnimation/Colour/" + nameof(FlashColourAnimation), order = 1)]
    internal class FlashColourAnimation : BaseAnimationObject
    {
        #region Serialized Fields

        [SerializeField] private float _flashDelaySeconds;

        #endregion

        #region Fields

        private int _colorKeyIndex = 0;

        #endregion

        #region Internal
        /// <summary>
        /// The method to override, when creating your own implementation.
        /// </summary>
        /// <param name="mesh">The mesh that will be given to modify.</param>
        /// <returns>The modified mesh.</returns>
        public override Mesh DoEffect(Mesh mesh)
        {
            Color[] colors = mesh.colors;
            for (int i = 0; i < mesh.colors.Length; i++)
            {
                colors[i] = Gradient.colorKeys[_colorKeyIndex].color;
            }
            if (_colorKeyIndex == 0)
            {
                _colorKeyIndex++;
            }
            else
            {
                _colorKeyIndex = 0;
            }
            mesh.colors = colors;
            return mesh;
        }

        #endregion

    }
}