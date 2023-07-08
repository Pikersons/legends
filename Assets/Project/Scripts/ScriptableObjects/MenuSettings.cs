using UnityEngine;
using DG.Tweening;

namespace Legends.ScriptableObjects
{
    [CreateAssetMenu(fileName = "MenuSettings", menuName = "Legends/MenuSettings")]
    public class MenuSettings : ScriptableObject
    {
        public Ease easeType;
        public float showSoundPitch;
        public float hideSoundPitch;
    }
}
