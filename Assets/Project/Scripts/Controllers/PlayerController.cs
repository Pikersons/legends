using Assets.Project.Scripts.Controllers;
using Fusion;
using Legends.Core.Models;
using Legends.Managers;
using Legends.ScriptableObjects;
using UnityEngine;

namespace Legends.Controllers
{
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private PlayerMoveController _characterMovementController;
        [SerializeField] private PlayerProjectileController _projectileController;
        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private Renderer _renderer;

        private PlayerRef _targetPlayerRef;

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

        public void Teste()
        {
            RpcInvokeInfo info = RPC_SetMaterial(1);
            Debug.Log(info);
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        public RpcInvokeInfo RPC_SetMaterial(int materialIndex)
        {
            _renderer.material = _playerSettings.Materials[materialIndex];
            return default;
        }

        public override void Spawned()
        {
            if (Object.HasInputAuthority)
            {
                GameManager.Instance.SetPlayerController(this);
            }
            GameManager.Instance.AddPlayer(Object.InputAuthority, this);
        }
    }
}
