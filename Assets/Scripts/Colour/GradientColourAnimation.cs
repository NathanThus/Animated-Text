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

        /// <summary>
        /// Applies a gradient over the entire mesh, moving right to left.
        /// </summary>
        /// <param name="mesh">The mesh to modify.</param>
        /// <returns>The modified mesh.</returns>
        private Mesh ApplyGradient(Mesh mesh)
        {
            Color[] newColors = mesh.colors;

            for (int i = 0; i < newColors.Length; i++)
            {
                newColors[i] = Gradient.Evaluate(Mathf.Repeat(Time.time + mesh.vertices[i].x * 0.001f, 1f));
            }

            mesh.colors = newColors;
            return mesh;
        }
        
        #endregion
    }
}