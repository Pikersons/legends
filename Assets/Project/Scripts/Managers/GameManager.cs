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
        private PlayerController _playerController;
        private CharacterMovementController _characterMovementController;

        private void Awake()
        {
            if (Instance != null)
            {
                throw new Exception("Trying to reset GameManager Singleton");
            }

            Instance = this;
        }

        public void SetPlayerController(PlayerController playerController)
        {
            _playerController = playerController;
            _playerCamera.SetTarget(_playerController.transform);

            if (_characterMovementController != null)
            {
                _characterMovementController.SetCharacter1(_playerController.GetComponent<NetworkObject>());
            }
        }

        public void SetCharacterMovementController(CharacterMovementController characterMovementController)
        {
            _characterMovementController = characterMovementController;

            if (_playerController != null)
            {
                _characterMovementController.SetCharacter1(_playerController.GetComponent<NetworkObject>());
            }
        }
    }
}


