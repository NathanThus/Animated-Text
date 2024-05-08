using System;
using UnityEngine;

namespace TextAnimation
{
    [Serializable]
    public struct VectorPair
    {
        [SerializeField, Tooltip("The minimum value for you to use.")] private Vector3 _minimum;
        [SerializeField, Tooltip("The maximum value for you to use.")] private Vector3 _maximum;

        public readonly Vector3 Minimum => _minimum;
        public readonly Vector3 Maximum => _maximum;
    }
}
