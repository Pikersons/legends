using Fusion;
using Legends.Managers;
using System;
using Assets.Project.Scripts.Controllers;
using Legends.Core.Models;
using UnityEngine;
using UnityEngine.AI;

namespace Legends.Controllers
{

    public class PlayerController : NetworkBehaviour
    {
        [SerializeField]
        private CharacterMovementController _characterMovementController;
        
        [SerializeField]
        private ProjectileController _projectileController;

        private PlayerRef _targetPlayerRef;
            
        [Networked]
        public int Life { get; set; }

        public override void Spawned()
        {
            if (Object.HasInputAuthority)
            {
                GameManager.Instance.SetPlayerController(this);
            }
            GameManager.Instance.AddPlayer(Object.InputAuthority, this);
        }

        public override void FixedUpdateNetwork()
        {
            if (GetInput(out InputData inputData))
            {
                if (inputData.IsRightMouseDown)
                {
                    _targetPlayerRef = inputData.TargetPlayerRef;
                    _projectileController.SetTarget(_targetPlayerRef);
                    _characterMovementController.SetTarget(_targetPlayerRef);
                }

                if (inputData.IsLeftMouseDown)
                {
                    _projectileController.SetTarget(PlayerRef.None);
                    _characterMovementController.MoveTo(inputData.Destination);
                }
            }
            
            _characterMovementController.OnFixedUpdateNetwork();
            _projectileController.OnFixedUpdateNetwork();
        }
    }
}
