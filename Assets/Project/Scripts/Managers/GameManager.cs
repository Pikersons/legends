using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Project.Scripts.Controllers;
using Fusion;
using Legends.Controllers;
using UnityEngine;


namespace Legends.Managers
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance { get; private set; }

        [SerializeField] private CameraController _playerCamera;
        [SerializeField] private InputManager _inputManager;

        private PlayerController _playerController;

        private Dictionary<PlayerRef, PlayerController> _playerControllers;

        private void Awake()
        {
            if (Instance != null)
            {
                throw new Exception("Trying to reset GameManager Singleton");
            }

            Instance = this;
            _playerControllers = new Dictionary<PlayerRef, PlayerController>();
        }

        public void SetPlayerController(PlayerController playerController)
        {
            _playerController = playerController;
            _inputManager.SetPlayerRef(playerController.GetComponent<NetworkObject>().InputAuthority);
            _playerCamera.SetTarget(_playerController.transform);
        }

        public void AddPlayer(PlayerRef inputAuthority, PlayerController playerController)
        {
            _playerControllers.Add(inputAuthority, playerController);
        }

        public PlayerController GetPlayerController(PlayerRef targetPlayerRef)
        {
            return _playerControllers[targetPlayerRef];
        }
    }
}


