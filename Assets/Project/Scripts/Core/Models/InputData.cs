using Fusion;
using UnityEngine;

namespace Legends.Core.Models
{
    public readonly struct InputData : INetworkInput
    {
        public Vector3 Destination { get; }

        public InputData(Vector3 _destination)
        {
            Destination = _destination;
        }
    }
}
