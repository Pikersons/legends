using Fusion;
using UnityEngine;

namespace Legends.Core.Models
{
    public readonly struct InputData : INetworkInput
    {
        private const int BUTTON_FIRE = 1;
        private const int BUTTON_FIRE_ALT = 2;

        private readonly NetworkButtons _buttons;

        public Vector3 Destination { get; }
        public PlayerRef TargetPlayerRef { get; }
        public bool IsLeftMouseDown { get => _buttons.IsSet(BUTTON_FIRE); set => _buttons.Set(BUTTON_FIRE, value); }
        public bool IsRightMouseDown { get => _buttons.IsSet(BUTTON_FIRE_ALT); set => _buttons.Set(BUTTON_FIRE_ALT, value); }

        public InputData(Vector3 destination, PlayerRef targetPlayerRef, bool isLeftMouseDown, bool isRightMouseDown)
        {
            Destination = destination;
            TargetPlayerRef = targetPlayerRef;

            _buttons = new NetworkButtons();
            _buttons.Set(BUTTON_FIRE, isLeftMouseDown);
            _buttons.Set(BUTTON_FIRE_ALT, isRightMouseDown);
        }
    }
}
