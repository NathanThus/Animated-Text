using System;
using UnityEngine;

namespace TextAnimation.Common
{

    [CreateAssetMenu(fileName = nameof(GlowColourAnimation), menuName = "TextAnimation/Colour/" + nameof(GlowColourAnimation), order = 1)]
    internal class GlowColourAnimation : BaseAnimationObject
    {
        #region Internal
        /// <summary>
        /// The method to override, when creating your own implementation.
        /// </summary>
        /// <param name="mesh">The mesh that will be given to modify.</param>
        /// <returns>The modified mesh.</returns>
        public override Mesh DoEffect(Mesh mesh)
        {
            Color[] colors = mesh.colors;
            Array.Fill<Color>(colors, Gradient.Evaluate(Mathf.Repeat(Time.time, 1f)));
            mesh.colors = colors;
            return mesh;
        }

        #endregion
    }
}