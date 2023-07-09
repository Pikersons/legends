using UnityEngine;
using Fusion;
using NaughtyAttributes;
using Legends.ScriptableObjects;

namespace Legends.Managers
{
    public class NetworkManager : MonoBehaviour
    {
        #region Dependencies and Setup

        [field: SerializeField][field: ReadOnly] public NetworkRunner NetworkRunner { get; set; }

        [field: SerializeField][field: ReadOnly] public GameSceneManager GameSceneManager { get; set; }

        [field: SerializeField][field: ReadOnly] public GameNetworkData GameNetworkData { get; set; }

        private void Awake()
        {
            GetDependencies();
        }

        [Button("Setup")]
        private void GetDependencies()
        {
            NetworkRunner = GetComponent<NetworkRunner>();
            GameSceneManager = FindObjectOfType<GameSceneManager>();
            GameNetworkData = GameNetworkData = Resources.Load<GameNetworkData>("GameNetworkData");
        }

        #endregion

        public async void StartGame()
        {
            StartGameArgs startGameArgs = new StartGameArgs()
            {
                GameMode = GameNetworkData.gameMode,
                SessionName = GameNetworkData.roomName,
                PlayerCount = 4,
                SceneManager = GameSceneManager.GetComponent<INetworkSceneManager>()
            };

            StartGameResult result = await NetworkRunner.StartGame(startGameArgs);

            if (result.Ok)
            {
                NetworkRunner.SetActiveScene("CharacterSelection");
            }
            else
            {
                Debug.LogError($"Failed to start: {result.ShutdownReason}");
            }

        }
        
    }
}
