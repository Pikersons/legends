using Fusion;
using UnityEngine;

namespace Legends.Core.Models
{
    public readonly struct InputData : INetworkInput
    {
        private const int _buttonPrimary = 1;
        private const int _buttonSecondary = 2;

        private readonly NetworkButtons _buttons;

        public Vector3 Destination { get; }
        public PlayerRef TargetPlayerRef { get; }
        public bool IsPrimaryDown { get => _buttons.IsSet(_buttonPrimary); set => _buttons.Set(_buttonPrimary, value); }
        public bool IsSecondaryDown { get => _buttons.IsSet(_buttonSecondary); set => _buttons.Set(_buttonSecondary, value); }

        public InputData(Vector3 destination, PlayerRef targetPlayerRef, bool isLeftMouseDown, bool isRightMouseDown)
        {
            Destination = destination;
            TargetPlayerRef = targetPlayerRef;

            _buttons = new NetworkButtons();
            _buttons.Set(_buttonPrimary, isLeftMouseDown);
            _buttons.Set(_buttonSecondary, isRightMouseDown);
        }
    }
}
