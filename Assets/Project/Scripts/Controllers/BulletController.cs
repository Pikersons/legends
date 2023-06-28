using System;
using Fusion;
using UnityEngine;

namespace Legends.Controllers
{
    public class BulletController : NetworkBehaviour
    {
        public event Action<NetworkObject> Collided;

        private void OnTriggerEnter(Collider other)
        {
            Collided?.Invoke(Object);
        }
    }
}
