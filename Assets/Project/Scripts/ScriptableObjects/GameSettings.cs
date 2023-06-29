using Fusion;
using UnityEngine;

namespace Legends.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Legends/GameSettings", order = 1)]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private GameMode _gameMode;

        public GameMode GameMode => _gameMode;
    }
}
