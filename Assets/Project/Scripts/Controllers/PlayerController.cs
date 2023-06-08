using Fusion;
using Legends.Managers;
using System;
using Legends.Core.Models;

namespace Legends.Controllers
{

    public class PlayerController : NetworkBehaviour
    {
        public override void Spawned()
        {

            if (Object.HasInputAuthority)
            {
                GameManager.Instance.SetPlayerController(this);
            }

        }
    }

}
