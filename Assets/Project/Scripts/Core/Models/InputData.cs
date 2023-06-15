using Fusion;
using UnityEngine;

namespace Legends.Core.Models
{
    public readonly struct InputData : INetworkInput
    {
        public Vector3 Destination { get; }

        public PlayerRef TargetPlayerRef { get; }

        public InputData(Vector3 destination, PlayerRef targetPlayerRef)
        {
            Destination = destination;
            TargetPlayerRef = targetPlayerRef;
        }
    }
}
