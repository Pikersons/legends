using System.Collections.Generic;
using Fusion;
using Legends.Core.Models;
using Legends.Managers;
using UnityEngine;

namespace Legends.Controllers
{
    public class PlayerProjectileController : NetworkBehaviour
    {
        [SerializeField] private Transform _firePosition;
        [SerializeField] private NetworkPrefabRef _bulletPrefab;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _bulletCooldown;

        private Dictionary<NetworkId, ProjectileData> _projectileDict;
        private PlayerRef _targetPlayerRef;
        private float _lastBulletTime;

        public override void FixedUpdateNetwork()
        {
            if (!Runner.IsServer)
            {
                return;
            }
            if (GetInput(out InputData inputData))
            {
                Runner.TryFindObject(inputData.TargetNetworkId, out NetworkObject targetNetworkObject);
                _targetPlayerRef = targetNetworkObject.InputAuthority;
            }
            if (_targetPlayerRef != PlayerRef.None &&
                _lastBulletTime + _bulletCooldown < Runner.SimulationTime)
            {
                SpawnProjectile();
            }
            foreach (KeyValuePair<NetworkId, ProjectileData> projectileDataKvp in _projectileDict)
            {
                ProjectileData projectileData = projectileDataKvp.Value;
                PlayerController targetController = GameManager.Instance.GetPlayerController(projectileData.TargetPlayerRef);
                Vector3 bulletPosition = projectileData.Object.transform.position;
                projectileData.Object.transform.position = Vector3
                    .MoveTowards(bulletPosition,
                                 targetController.transform.position,
                                 _bulletSpeed * Runner.DeltaTime);
            }
        }

        #region Unity
        private void Awake()
        {
            _projectileDict = new Dictionary<NetworkId, ProjectileData>();
        }
        #endregion

        private void BulletController_Collided(NetworkObject bulletObject)
        {
            _projectileDict.Remove(bulletObject.Id);
            Runner.Despawn(bulletObject);
        }

        private void SpawnProjectile()
        {
            NetworkObject projectileObject = Runner.Spawn(_bulletPrefab, _firePosition.position);
            BulletController bulletController = projectileObject.GetComponent<BulletController>();
            bulletController.Collided += BulletController_Collided;
            _projectileDict.Add(projectileObject.Id, new ProjectileData(projectileObject, _targetPlayerRef));
            _lastBulletTime = Runner.SimulationTime;
        }
    }
}
