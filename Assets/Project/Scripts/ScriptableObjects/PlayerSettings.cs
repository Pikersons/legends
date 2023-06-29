using UnityEngine;

namespace Legends.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Legends/PlayerSettings", order = 2)]
    public class PlayerSettings : ScriptableObject
    {
        [SerializeField] private Material[] _materials;

        public Material[] Materials => _materials;
    }
}
