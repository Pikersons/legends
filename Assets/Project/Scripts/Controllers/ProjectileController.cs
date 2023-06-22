using System.Collections.Generic;
using Fusion;
using Legends.Managers;
using UnityEngine;

namespace Legends.Controllers
{
    public class ProjectileController : NetworkBehaviour
    {
        [SerializeField]
        private NetworkPrefabRef _bulletPrefab;

        [SerializeField]
        private Transform _firePosition;

        [SerializeField]
        private float _bulletSpeed;

        [SerializeField]
        private float _bulletCooldown;

        private float _lastBulletTime;

        private PlayerRef _targetPlayerRef;

        private Dictionary<NetworkId, ProjectileData> _projectileDict;

        private void Awake()
        {
            _projectileDict = new Dictionary<NetworkId, ProjectileData>();
        }

        public void SetTarget(PlayerRef targetPlayerRef)
        {
            _targetPlayerRef = targetPlayerRef;
        }

        public void OnFixedUpdateNetwork()
        {
            if (!Runner.IsServer)
            {
                return;
            }

            if (_targetPlayerRef != PlayerRef.None
                && _lastBulletTime + _bulletCooldown < Runner.SimulationTime)
            {
                SpawnProjectile();
            }

            foreach (KeyValuePair<NetworkId,ProjectileData> projectileDataKvp in _projectileDict)
            {
                ProjectileData projectileData = projectileDataKvp.Value;
                PlayerController targetController =
                    GameManager.Instance.GetPlayerController(projectileData.TargetPlayerRef);

                Vector3 bulletPosition = projectileData.Object.transform.position;
                projectileData.Object.transform.position = Vector3.MoveTowards(
                    bulletPosition,
                    targetController.transform.position,
                    _bulletSpeed * Runner.DeltaTime);
            }
        }

        private void SpawnProjectile()
        {
            NetworkObject projectileObject = Runner.Spawn(_bulletPrefab, _firePosition.position);
            BulletController bulletController = projectileObject.GetComponent<BulletController>();
            bulletController.Collided += BulletController_Collided;
            _projectileDict.Add(projectileObject.Id, new ProjectileData(projectileObject, _targetPlayerRef));
            _lastBulletTime = Runner.SimulationTime;
        }

        private void BulletController_Collided(NetworkObject bulletObject)
        {
            _projectileDict.Remove(bulletObject.Id);
            Runner.Despawn(bulletObject);
        }
    }
}