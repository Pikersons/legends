using Fusion;
using UnityEngine;

namespace Legends.Core.Models
{
    public enum InputButton
    {
        PrimaryButton = 0,
        SecondaryButton = 1,
    }

    public readonly struct InputData : INetworkInput
    {
        private readonly NetworkButtons _buttons;

        public readonly Vector3 PointerScreenPosition { get; }
        public readonly Vector3 PointerWorldPosition { get; }
        public readonly bool IsPrimaryButtonDown => _buttons.IsSet(InputButton.PrimaryButton);
        public readonly bool IsSecondaryButtonDown => _buttons.IsSet(InputButton.SecondaryButton);

        public InputData(Vector3 pointerScreenPosition,
                         Vector3 pointerWorldPosition,
                         bool isPrimaryButtonDown,
                         bool isSecondaryButtonDown)
        {
            PointerScreenPosition = pointerScreenPosition;
            PointerWorldPosition = pointerWorldPosition;
            _buttons = new NetworkButtons();
            _buttons.Set(InputButton.PrimaryButton, isPrimaryButtonDown);
            _buttons.Set(InputButton.SecondaryButton, isSecondaryButtonDown);
        }
    }
}
