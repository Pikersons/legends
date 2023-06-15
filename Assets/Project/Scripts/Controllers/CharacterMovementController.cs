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
        private int _target;

        public override void FixedUpdateNetwork()
        {
            if (GetInput(out InputData inputData))
            {
                _navMeshAgent.destination = inputData.Destination;
                _target = inputData.TargetPlayerRef.RawEncoded;
            }
        }
    }
}
