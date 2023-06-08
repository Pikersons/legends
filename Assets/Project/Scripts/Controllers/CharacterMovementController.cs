using Fusion;
using Legends.Core.Models;
using Legends.Managers;
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

        public override void Spawned()
        {
            if (HasInputAuthority)
            {
                GameManager.Instance.SetCharacterMovementController(this);
            }
        }

        public override void FixedUpdateNetwork()
        {
            if (_character1 != null && GetInput(out InputData inputData))
            {
                _character1.destination = inputData.Destination;
            }
        }
    }
}
