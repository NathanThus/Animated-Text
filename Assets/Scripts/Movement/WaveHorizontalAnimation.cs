using System;
using UnityEngine;
using TextAnimation.Common;

namespace TextAnimation.Movement
{

    [CreateAssetMenu(fileName = nameof(WaveHorizontalAnimation), menuName = "TextAnimation/Movement/" + nameof(WaveHorizontalAnimation), order = 1)]
    internal class WaveHorizontalAnimation : BaseAnimationObject
    {
        #region Internal
        /// <summary>
        /// The method to override, when creating your own implementation.
        /// </summary>
        /// <param name="mesh">The mesh that will be given to modify.</param>
        /// <returns>The modified mesh.</returns>
        public override Mesh DoEffect(Mesh mesh)
        {
            Vector3[] newVertices = mesh.vertices;
            float currentTime = Time.time;
            for (var i = 0; i < newVertices.Length / 4; i++)
            {
                Vector3 Coordinates = WaveHorizontal(currentTime + (i * TranslationPair.Maximum.x), TranslationPair.Maximum.y);
                for (int j = 0; j < 4; j++)
                {
                    newVertices[i*4+j] += Coordinates;
                }
            }

            mesh.vertices = newVertices;
            return mesh;
        }

        protected Vector3 WaveHorizontal(float time, float amplitude)
        {
            return new Vector3(MathF.Sin(time) * amplitude, 0, 0);
        }

        #endregion

        #region Private

        #endregion
    }
}