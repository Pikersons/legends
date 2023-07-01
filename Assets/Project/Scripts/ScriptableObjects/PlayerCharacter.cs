using Fusion;
using UnityEngine;

namespace Legends.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerCharacter", menuName = "Legends/PlayerCharacter", order = 2)]
    public class PlayerCharacter : ScriptableObject
    {
        [SerializeField] private NetworkPrefabRef _playerPrefab;

        public NetworkPrefabRef PlayerPrefab => _playerPrefab;
    }
}
