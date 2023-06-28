using System;
using System.Collections.Generic;
using Fusion;
using Legends.Controllers;
using Legends.ScriptableObjects;
using UnityEngine;

namespace Legends.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Network")]
        [SerializeField] private NetworkController _networkController;
        [SerializeField] private NetworkRunner _networkRunner;

        [Header("Player")]
        [SerializeField] private CameraController _playerCamera;
        [SerializeField] private Material[] _materials;
        [SerializeField] private NetworkPrefabRef _playerPrefab;

        [Header("Game")]
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private InputManager _inputManager;

        private PlayerController _playerController;

        private Dictionary<PlayerRef, PlayerController> _playerControllers;

        public void SetPlayerController(PlayerController playerController)
        {
            _playerController = playerController;
            _inputManager.SetPlayerRef(playerController.GetComponent<NetworkObject>().InputAuthority);
            _playerCamera.SetTarget(_playerController.transform);
        }

        public void AddPlayer(PlayerRef inputAuthority, PlayerController playerController)
        {
            playerController.SetMaterial(_materials[_playerControllers.Count]);
            _playerControllers.Add(inputAuthority, playerController);
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

        private void NetworkController_PlayerJoined(PlayerRef player)
        {
            if (_networkRunner.IsServer && !_playerControllers.ContainsKey(player))
            {
                NetworkObject networkObject = _networkRunner
                    .Spawn(_playerPrefab,
                           position: Vector3.up,
                           inputAuthority: player,
                           onBeforeSpawned: (networkRunner, @object) =>
                           {
                               Debug.Log($"RODOU NO CLIENT? - {networkRunner.IsServer} - {networkRunner.IsClient} - {@object.HasInputAuthority}");
                           });
                PlayerController playerController = networkObject.GetComponent<PlayerController>();
                playerController.Life = UnityEngine.Random.Range(50, 100);
            }
        }
        #endregion
    }
}
