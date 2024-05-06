using TMPro;
using UnityEngine;

namespace TextAnimation
{
    public abstract class EffectScriptableObject : ScriptableObject
    {
        public abstract Mesh DoEffect(Mesh Mesh); 
        
    }
}