using Assets.Project.Scripts.Controllers;
using Fusion;
using Legends.Core.Models;
using Legends.Managers;
using UnityEngine;

namespace Legends.Controllers
{
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private PlayerMoveController _characterMovementController;
        [SerializeField] private PlayerProjectileController _projectileController;

        public override void FixedUpdateNetwork()
        {
            if (GetInput(out InputData inputData))
            {
                Debug.Log($"Player - {inputData.IsPrimaryButtonDown} - {inputData.IsSecondaryButtonDown} - {inputData.PointerWorldPosition} - {inputData.HoverNetworkId} - {inputData.SelectedNetworkId} - {inputData.TargetNetworkId}");
            }
        }

        public override void Spawned()
        {
            GameManager.Instance.AddPlayer(this);
        }
    }
}
