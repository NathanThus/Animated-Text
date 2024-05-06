using UnityEngine;

namespace TextAnimation
{

    [CreateAssetMenu(fileName = "AnimationScriptableObject", menuName = "TextAnimation/AnimationScriptableObject", order = 1)]
    public class AnimationScriptableObject : ScriptableObject
    {
        public string Identifier;
        public EffectScriptableObject effect;
    }
}