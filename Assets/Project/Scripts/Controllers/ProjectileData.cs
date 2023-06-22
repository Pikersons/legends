using Fusion;

namespace Legends.Controllers
{
    public struct ProjectileData
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