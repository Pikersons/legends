using Fusion;

namespace Legends.Core.Models
{
    public readonly struct ProjectileData
    {
        public NetworkObject Object { get; }
        public PlayerRef TargetPlayerRef { get; }

        public ProjectileData(NetworkObject projectileObject, PlayerRef targetPlayerRef)
        {
            Object = projectileObject;
            TargetPlayerRef = targetPlayerRef;
        }
    }
}