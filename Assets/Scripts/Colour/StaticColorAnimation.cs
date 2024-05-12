using System;
using UnityEngine;
using TextAnimation.Common;

namespace TextAnimation.Colour
{

    [CreateAssetMenu(fileName = nameof(StaticColorAnimation), menuName = "TextAnimation/Colour/" + nameof(StaticColorAnimation), order = 1)]
    internal class StaticColorAnimation : BaseAnimationObject
    {
        #region Internal
        /// <summary>
        /// The method to override, when creating your own implementation.
        /// </summary>
        /// <param name="mesh">The mesh that will be given to modify.</param>
        /// <returns>The modified mesh.</returns>
        public override Mesh DoEffect(Mesh mesh)
        {
            return ColourByWhole(mesh);
        }

        #endregion

        #region Private

        #endregion
    }
}