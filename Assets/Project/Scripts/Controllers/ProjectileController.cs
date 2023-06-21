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

        private List<NetworkObject> _bulletList;

        private void Awake()
        {
            _bulletList = new List<NetworkObject>();
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
                NetworkObject projectileObject = Runner.Spawn(_bulletPrefab, _firePosition.position);
                BulletController bulletController = projectileObject.GetComponent<BulletController>();
                bulletController.Collided += BulletControllet_Collided;
                _bulletList.Add(projectileObject);
                _lastBulletTime = Runner.SimulationTime;
            }

            for (int i = 0; i < _bulletList.Count; i++)
            {
                NetworkObject bulletObject = _bulletList[i];
                PlayerController targetController = GameManager.Instance.GetPlayerController(_targetPlayerRef);

                Vector3 bulletPosition = bulletObject.transform.position;
                bulletObject.transform.position = Vector3.MoveTowards(
                    bulletPosition,
                    targetController.transform.position,
                    _bulletSpeed * Runner.DeltaTime);
            }
        }

        private void BulletControllet_Collided(NetworkObject bulletObject)
        {
            _bulletList.Remove(bulletObject);
            Runner.Despawn(bulletObject);
        }
    }
}