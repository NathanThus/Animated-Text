using System;
using UnityEngine;
using TextAnimation.Common;

namespace TextAnimation.Movement
{

    [CreateAssetMenu(fileName = nameof(WaveVerticalAnimation), menuName = "TextAnimation/Movement/" + nameof(WaveVerticalAnimation), order = 1)]
    internal class WaveVerticalAnimation : BaseAnimationObject
    {
        #region Internal
        /// <summary>
        /// The method to override, when creating your own implementation.
        /// </summary>
        /// <param name="mesh">The mesh that will be given to modify.</param>
        /// <returns>The modified mesh.</returns>
        public override Mesh DoEffect(Mesh mesh)
        {
            return WaveVertical(mesh);
        }

        #endregion

        #region Private

        /// <summary>
        /// Makes the mesh oscillate vertically.
        /// </summary>
        /// <param name="mesh">The mesh to modify.</param>
        /// <returns>The modified mesh.</returns>
        private Mesh WaveVertical(Mesh mesh)
        {
            Vector3[] newVertices = mesh.vertices;
            float currentTime = Time.time;
            for (var i = 0; i < newVertices.Length / 4; i++)
            {
                Vector3 Coordinates = Wave(currentTime + (i * TranslationPair.Maximum.x), TranslationPair.Maximum.y);
                for (int j = 0; j < 4; j++)
                {
                    newVertices[i * 4 + j] += Coordinates;
                }
            }

            mesh.vertices = newVertices;
            return mesh;
        }

        #endregion

        #region Private

        private Vector3 Wave(float time, float amplitude)
        {
            return new Vector3(0, MathF.Sin(time) * amplitude, 0);
        }

        #endregion
    }
}