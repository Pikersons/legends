using Fusion;
using Legends.Controllers;
using Legends.Core.Models;
using Legends.Managers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Project.Scripts.Controllers
{
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField]
        private NavMeshAgent _navMeshAgent;

        private PlayerRef _targetPlayerRef;

        private bool _hasDestination;

        public void OnFixedUpdateNetwork()
        {
            if (_targetPlayerRef.IsNone == false)
            {
                _navMeshAgent.isStopped = false;
                _hasDestination = false;
                PlayerController playerController = GameManager.Instance.GetPlayerController(_targetPlayerRef);

                Vector3 dif = playerController.transform.position - transform.position;
                Vector3 targetPostion = dif.magnitude < 5.0f ? transform.position : playerController.transform.position - dif.normalized * 5.0f;

                _navMeshAgent.destination = targetPostion;
            }

            if (_targetPlayerRef.IsNone && (_navMeshAgent.remainingDistance <= 0 || _hasDestination == false))
            {
                _hasDestination = false;
                _navMeshAgent.isStopped = true;
            }
        }

        public void SetTarget(PlayerRef playerRef)
        {
            _targetPlayerRef = playerRef;
        }

        public void MoveTo(Vector3 destination)
        {
            _navMeshAgent.isStopped = false;
            _hasDestination = true;
            _navMeshAgent.destination = destination;
            _targetPlayerRef = PlayerRef.None;
        }
    }
}
