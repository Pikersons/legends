using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Legends.Managers;

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
