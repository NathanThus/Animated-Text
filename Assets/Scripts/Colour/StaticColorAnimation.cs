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

        /// <summary>
        /// Colour the whole mesh a given colour.
        /// </summary>
        /// <param name="mesh">The mesh to modify.</param>
        /// <returns>The modified mesh.</returns>
        private Mesh ColourByWhole(Mesh mesh)
        {
            Color[] colors = mesh.colors;
            Array.Fill<Color>(colors, Color);
            mesh.colors = colors;
            return mesh;
        }

        #endregion
    }
}