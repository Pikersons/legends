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
        [SerializeField] private PlayerCharacter _playerCharacter;

        [Header("Game")]
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private InputManager _inputManager;

        private Dictionary<PlayerRef, PlayerController> _playerControllers;

        public void AddPlayer(PlayerController playerController)
        {
            if (playerController.Object.HasInputAuthority)
            {
                _playerCamera.SetTarget(playerController.transform);
            }
            _playerControllers.Add(playerController.Object.InputAuthority, playerController);
        }

        public PlayerController GetPlayerController(PlayerRef targetPlayerRef)
        {
            return _playerControllers[targetPlayerRef];
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
                }
                else
                {
                    Debug.Log($"Connected - {player}");
                    _networkRunner.Spawn(_playerCharacter.PlayerPrefab, // mudar para escolha do player
                                         inputAuthority: player,
                                         position: Vector3.up);
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
