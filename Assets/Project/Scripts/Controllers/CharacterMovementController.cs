using Fusion;
using Legends.Controllers;
using Legends.Core.Models;
using Legends.Managers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Project.Scripts.Controllers
{
    public class CharacterMovementController : NetworkBehaviour
    {
        [SerializeField]
        private NavMeshAgent _navMeshAgent;

        [SerializeField]
        private int _targetId = -1;

        private PlayerRef _targetPlayerRef;

        public override void FixedUpdateNetwork()
        {
            if (GetInput(out InputData inputData))
            {
                _targetId = inputData.TargetPlayerRef.PlayerId;
                _targetPlayerRef = inputData.TargetPlayerRef;
                if (_targetPlayerRef.IsNone == false)
                {
                    PlayerController playerController = GameManager.Instance.GetPlayerController(_targetPlayerRef);
                    _navMeshAgent.destination = playerController.transform.position;
                }
                else if(inputData.IsLeftMouseDown)
                {
                    _navMeshAgent.destination = inputData.Destination;
                }
                else if (_navMeshAgent.remainingDistance <= 0)
                {
                    _navMeshAgent.isStopped = true;
                }
            }
        }
    }
}
