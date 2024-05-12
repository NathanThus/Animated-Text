using System;
using UnityEngine;
using TextAnimation.Common;

namespace TextAnimation.Movement
{

    [CreateAssetMenu(fileName = nameof(WaveAnimation), menuName = "TextAnimation/Movement/" + nameof(WaveAnimation), order = 1)]
    internal class WaveAnimation : BaseAnimationObject
    {
        #region Internal
        /// <summary>
        /// The method to override, when creating your own implementation.
        /// </summary>
        /// <param name="mesh">The mesh that will be given to modify.</param>
        /// <returns>The modified mesh.</returns>
        public override Mesh DoEffect(Mesh mesh)
        {
            return WaveAnimation(mesh);
        }

        #endregion

        #region Private

        #endregion
    }
}