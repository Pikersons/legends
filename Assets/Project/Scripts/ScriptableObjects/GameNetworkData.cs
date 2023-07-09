using Fusion;
using UnityEngine;

namespace Legends.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameNetworkData", menuName = "Legends/GameNetworkData")]
    public class GameNetworkData : ScriptableObject
    {
        public GameMode gameMode;
        public string roomName;
    }
}
