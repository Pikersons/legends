using Fusion;
using Legends.Core.Models;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Project.Scripts.Controllers
{
    public class PlayerMoveController : NetworkBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;

        public override void FixedUpdateNetwork()
        {
            if (GetInput(out InputData inputData))
            {
                if (inputData.IsSecondaryButtonDown)
                {
                    _navMeshAgent.isStopped = false;
                    _navMeshAgent.destination = inputData.PointerWorldPosition;
                    _navMeshAgent.stoppingDistance = 1;
                }
            }
        }
    }
}
