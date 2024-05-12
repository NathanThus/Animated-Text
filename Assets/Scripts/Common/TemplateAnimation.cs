using System;
using UnityEngine;

namespace TextAnimation.Common
{

    [CreateAssetMenu(fileName = nameof(TemplateAnimation), menuName = "TextAnimation/Unfiltered/" + nameof(TemplateAnimation), order = 1)]
    internal class TemplateAnimation : BaseAnimationObject
    {
        #region Internal
        /// <summary>
        /// The method to override, when creating your own implementation.
        /// </summary>
        /// <param name="mesh">The mesh that will be given to modify.</param>
        /// <returns>The modified mesh.</returns>
        public override Mesh DoEffect(Mesh mesh)
        {
            return mesh; // Currently, does
        }

        #endregion

        #region Private

        #endregion
    }
}