using System;
using System.Collections.Generic;
using Fusion;
using Legends.Controllers;
using Legends.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Legends.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Network")]
        [SerializeField] private NetworkController _networkController;
        [SerializeField] private NetworkRunner _networkRunner;
        [SerializeField] private NetworkSceneManagerDefault _networkSceneManager;

        [Header("Player")]
        [SerializeField] private CameraController _playerCamera;
        [SerializeField] private NetworkPrefabRef _playerPrefab;

        [Header("Game")]
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private InputManager _inputManager;

        private Dictionary<PlayerRef, PlayerController> _playerControllers;

        public void AddPlayer(PlayerRef inputAuthority, PlayerController playerController)
        {
            _playerControllers.Add(inputAuthority, playerController);
        }

        public PlayerController GetPlayerController(PlayerRef targetPlayerRef)
        {
            return _playerControllers[targetPlayerRef];
        }

        public void SetPlayerController(PlayerController playerController)
        {
            //_inputManager.SetPlayerRef(playerController.GetComponent<NetworkObject>().InputAuthority);
            _playerCamera.SetTarget(playerController.transform);
        }

        #region Unity
        private void Awake()
        {
            if (Instance != null)
            {
                throw new Exception("Trying to reset GameManager Singleton");
            }

            Instance = this;
            _networkController.PlayerJoined += NetworkController_PlayerJoined;
            _playerControllers = new Dictionary<PlayerRef, PlayerController>();
        }

        private void Start()
        {
            StartGame(_gameSettings.GameMode);
        }
        #endregion

        private void NetworkController_PlayerJoined(PlayerRef player)
        {
            if (_networkRunner.IsServer)
            {
                if (_playerControllers.TryGetValue(player, out PlayerController playerController))
                {
                    Debug.Log($"Reconncted - {player}");
                    playerController.Teste();
                }
                else
                {
                    Debug.Log($"Connected - {player}");
                    NetworkObject networkObject = _networkRunner
                        .Spawn(_playerPrefab,
                               position: Vector3.up,
                               inputAuthority: player);
                    playerController = networkObject.GetComponent<PlayerController>();
                    playerController.Teste();
                }
            }
        }

        private async void StartGame(GameMode gameMode)
        {
            _networkRunner.ProvideInput = true;
            await _networkRunner.StartGame(new StartGameArgs()
            {
                GameMode = gameMode,
                SessionName = "Pikersons",
                Scene = SceneManager.GetActiveScene().buildIndex,
                SceneManager = _networkSceneManager
            });
        }
    }
}
