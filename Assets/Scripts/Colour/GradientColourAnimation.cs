using System;
using UnityEngine;

namespace TextAnimation.Common
{

    [CreateAssetMenu(fileName = nameof(GradientColourAnimation), menuName = "TextAnimation/Colour/" + nameof(GradientColourAnimation), order = 1)]
    internal class GradientColourAnimation : BaseAnimationObject
    {
        #region Internal
        /// <summary>
        /// The method to override, when creating your own implementation.
        /// </summary>
        /// <param name="mesh">The mesh that will be given to modify.</param>
        /// <returns>The modified mesh.</returns>
        public override Mesh DoEffect(Mesh mesh)
        {
            return ApplyGradient(mesh);
        }

        #endregion

        #region Private

        #endregion
    }
}