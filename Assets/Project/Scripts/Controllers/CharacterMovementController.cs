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

        private bool _hasDestination;

        public override void FixedUpdateNetwork()
        {
            if (GetInput(out InputData inputData))
            {
                _targetId = inputData.TargetPlayerRef.PlayerId;
                _targetPlayerRef = inputData.TargetPlayerRef;

                if (_targetPlayerRef.IsNone == false)
                {
                    PlayerController playerController = GameManager.Instance.GetPlayerController(_targetPlayerRef);
                    _navMeshAgent.isStopped = false;
                    _hasDestination = false;
                    _navMeshAgent.destination = playerController.transform.position;
                }
                
                if(inputData.IsLeftMouseDown == true)
                {
                    _navMeshAgent.destination = inputData.Destination;
                    _navMeshAgent.isStopped = false;
                    _hasDestination = true;
                    _targetPlayerRef = PlayerRef.None;
                }
                
                if (_targetPlayerRef.IsNone == true
                    && inputData.IsLeftMouseDown == false
                    && (_navMeshAgent.remainingDistance <= 0 || _hasDestination == false))
                {
                    _hasDestination = false;
                    _navMeshAgent.isStopped = true;
                }
            }
        }
    }
}
