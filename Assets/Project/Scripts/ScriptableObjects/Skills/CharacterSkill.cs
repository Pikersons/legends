using UnityEngine;

namespace Legends.ScriptableObjects.Skills
{
    [CreateAssetMenu(menuName = "Project/ScriptableObjects/CharacterSkill", order = 2)]
    public class CharacterSkill : ScriptableObject
    {
        [SerializeField] private int _damage;
        [SerializeField] private int _resource;
    }
}
