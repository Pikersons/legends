using Fusion;
using Legends.Managers;
using System;
using Legends.Core.Models;
using UnityEngine;
using UnityEngine.AI;

namespace Legends.Controllers
{

    public class PlayerController : NetworkBehaviour
    {
        [Networked]
        public int Life { get; set; }

        public override void Spawned()
        {
            if (Object.HasInputAuthority)
            {
                GameManager.Instance.SetPlayerController(this);
            }
        }
    }

}
