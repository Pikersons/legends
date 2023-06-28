using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using Legends.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Legends.Controllers
{
    public class NetworkController : MonoBehaviour, INetworkRunnerCallbacks
    {
        public event Action<PlayerRef> PlayerJoined;

        [SerializeField] private NetworkRunner _runner;
        [SerializeField] private NetworkSceneManagerDefault _networkSceneManager;
        [Space]
        [SerializeField] private InputManager _inputManager;

        private Dictionary<PlayerRef, PlayerController> _characters;

        public void OnConnectedToServer(NetworkRunner runner)
        { }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        { }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
        { }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        { }

        public void OnDisconnectedFromServer(NetworkRunner runner)
        { }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        { }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            _inputManager.PopulateInput(input);
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        { }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            PlayerJoined?.Invoke(player);
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        { }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
        { }

        public void OnSceneLoadDone(NetworkRunner runner)
        { }

        public void OnSceneLoadStart(NetworkRunner runner)
        { }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        { }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        { }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        { }

        #region Unity
        private void Awake()
        {
            _characters = new Dictionary<PlayerRef, PlayerController> { };
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 200, 40), "Host"))
            {
                StartGame(GameMode.Host);
            }
            if (GUI.Button(new Rect(0, 40, 200, 40), "Join"))
            {
                StartGame(GameMode.Client);
            }
        }
        #endregion

        private async void StartGame(GameMode gameMode)
        {
            _runner.ProvideInput = true;

            await _runner.StartGame(new StartGameArgs()
            {
                GameMode = gameMode,
                SessionName = "Pikersons",
                Scene = SceneManager.GetActiveScene().buildIndex,
                SceneManager = _networkSceneManager
            });
        }
    }
}
