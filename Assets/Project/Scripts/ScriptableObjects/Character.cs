using Legends.ScriptableObjects.Skills;
using UnityEngine;

namespace Legends.ScriptableObjects
{
    public enum ResourceType
    {
        Mana,
    }

    [CreateAssetMenu(menuName = "Project/ScriptableObjects/Character", order = 1)]
    public class Character : ScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        [Space]
        [SerializeField] private string _name;
        [SerializeField] private int _level;
        [Space]
        [SerializeField] private int _life;
        [SerializeField] private int _resource;
        [SerializeField] private int _resourceRegen;
        [SerializeField] private ResourceType _resourceType;
        [Space]
        [SerializeField] private CharacterSkill _characterSkill1;
        [SerializeField] private CharacterSkill _characterSkill2;
        [SerializeField] private CharacterSkill _characterSkill3;
        [SerializeField] private CharacterSkill _characterSkillSpecial;
    }
}
