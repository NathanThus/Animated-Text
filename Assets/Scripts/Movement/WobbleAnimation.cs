using System;
using UnityEngine;
using TextAnimation.Common;

namespace TextAnimation.Movement
{

    [CreateAssetMenu(fileName = nameof(WobbleAnimation), menuName = "TextAnimation/Movement/" + nameof(WobbleAnimation), order = 1)]
    internal class WobbleAnimation : BaseAnimationObject
    {
        #region Internal
        /// <summary>
        /// The method to override, when creating your own implementation.
        /// </summary>
        /// <param name="mesh">The mesh that will be given to modify.</param>
        /// <returns>The modified mesh.</returns>
        public override Mesh DoEffect(Mesh mesh)
        {
            return WobbleMesh(mesh);
        }

        #endregion

        #region Private

        /// <summary>
        /// Gives the mesh a jello-like consistency.
        /// </summary>
        /// <param name="mesh">The mesh to modify.</param>
        /// <returns>The modified mesh.</returns>
        private Mesh WobbleMesh(Mesh mesh)
        {
            Vector3[] newVertices = mesh.vertices;
            for (var i = 0; i < mesh.vertices.Length; i++)
            {
                newVertices[i] += Wobble(Time.time + i,
                                         TranslationPair.Maximum.x,
                                         TranslationPair.Maximum.y);
            }
            mesh.vertices = newVertices;
            return mesh;
        }

        protected Vector3 Wobble(float time, float amplitudeX, float amplitudeY)
        {
            return new Vector3(MathF.Sin(time) * amplitudeX, Mathf.Cos(time) * amplitudeY, 0);
        }

        #endregion
    }
}