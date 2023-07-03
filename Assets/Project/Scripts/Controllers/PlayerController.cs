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
            _projectileController.OnFixedUpdateNetwork();
        }

        public override void Spawned()
        {
            GameManager.Instance.AddPlayer(this);
        }
    }
}
