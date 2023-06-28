using Assets.Project.Scripts.Controllers;
using Fusion;
using Legends.Core.Models;
using Legends.Managers;
using UnityEngine;

namespace Legends.Controllers
{
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField]
        private PlayerMoveController _characterMovementController;

        [SerializeField]
        private PlayerProjectileController _projectileController;

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
                if (inputData.IsSecondaryDown)
                {
                    _targetPlayerRef = inputData.TargetPlayerRef;
                    _projectileController.SetTarget(_targetPlayerRef);
                    _characterMovementController.SetTarget(_targetPlayerRef);
                }

                if (inputData.IsPrimaryDown)
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
