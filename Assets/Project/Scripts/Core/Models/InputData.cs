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

        public readonly NetworkId HoverNetworkId { get; }
        public readonly NetworkId SelectedNetworkId { get; }
        public readonly NetworkId TargetNetworkId { get; }
        public readonly Vector3 PointerWorldPosition { get; }
        public readonly bool IsPrimaryButtonDown => _buttons.IsSet(InputButton.PrimaryButton);
        public readonly bool IsSecondaryButtonDown => _buttons.IsSet(InputButton.SecondaryButton);

        public InputData(NetworkId hoverNetworkId,
                         NetworkId selectedNetworkId,
                         NetworkId targetNetworkId,
                         Vector3 pointerWorldPosition,
                         bool isPrimaryButtonDown,
                         bool isSecondaryButtonDown)
        {
            HoverNetworkId = hoverNetworkId;
            SelectedNetworkId = selectedNetworkId;
            TargetNetworkId = targetNetworkId;
            PointerWorldPosition = pointerWorldPosition;
            _buttons = new NetworkButtons();
            _buttons.Set(InputButton.PrimaryButton, isPrimaryButtonDown);
            _buttons.Set(InputButton.SecondaryButton, isSecondaryButtonDown);
        }
    }
}
