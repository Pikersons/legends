using Assets.Project.Scripts.Core.Models;
using Fusion;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Project.Scripts.Controllers
{
    public class CharacterMovementController : NetworkBehaviour
    {
        [SerializeField] private NavMeshAgent _character1;

        public void SetCharacter1(NetworkObject character)
        {
            _character1 = character.GetComponent<NavMeshAgent>();
        }

        public override void FixedUpdateNetwork()
        {
            base.FixedUpdateNetwork();
            if (GetInput(out InputData inputData))
            {
                _character1.destination = inputData.Destination;
            }
        }
    }
}
