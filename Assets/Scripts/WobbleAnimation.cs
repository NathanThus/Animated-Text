using System;
using UnityEngine;

namespace TextAnimation
{

    [CreateAssetMenu(fileName = nameof(TextAnimation.WobbleAnimation), menuName = "TextAnimation/Movement/" + nameof(TextAnimation.WobbleAnimation), order = 1)]
    internal class WobbleAnimation : BaseAnimationObject
    {
        #region Internal
        /// <summary>
        /// The method to override, when creating your own implementation.
        /// </summary>
        /// <param name="mesh">The mesh that will be given to modify.</param>
        /// <returns>The modified mesh.</returns>
        internal override Mesh DoEffect(Mesh mesh)
        {
            return WobbleAnimation(mesh);
        }

        #endregion

        #region Private

        #endregion
    }
}