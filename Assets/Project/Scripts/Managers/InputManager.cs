using Fusion;
using Legends.Core.Models;
using UnityEngine;

namespace Legends.Managers
{
    public class InputManager : MonoBehaviour
    {
        private NetworkId _selectedNetworkId;
        private NetworkId _targetNetworkId;
        private Vector3 _pointerScreenPosition;
        private Vector3 _pointerWorldPosition;
        private bool _isInputChanged;
        private bool _isPrimaryButtonDown;
        private bool _isSecondaryButtonDown;

        public NetworkId SelectedNetworkId
        {
            get => _selectedNetworkId;
            set
            {
                _selectedNetworkId = value;
                _isInputChanged = true;
            }
        }

        public NetworkId TargetNetworkId
        {
            get => _targetNetworkId;
            set
            {
                _targetNetworkId = value;
                _isInputChanged = true;
            }
        }

        public Vector3 PointerScreenPosition
        {
            get => _pointerScreenPosition;
            set
            {
                _pointerScreenPosition = value;
                _isInputChanged = true;
            }
        }

        public Vector3 PointerWorldPosition
        {
            get => _pointerWorldPosition;
            set
            {
                _pointerWorldPosition = value;
                _isInputChanged = true;
            }
        }

        public bool IsPrimaryButtonDown
        {
            get => _isPrimaryButtonDown;
            set
            {
                _isPrimaryButtonDown = value;
                _isInputChanged = true;
            }
        }

        public bool IsSecondaryButtonDown
        {
            get => _isSecondaryButtonDown;
            set
            {
                _isSecondaryButtonDown = value;
                _isInputChanged = true;
            }
        }

        public void PopulateInput(NetworkInput input)
        {
            if (_isInputChanged)
            {
                InputData data = new(SelectedNetworkId,
                                     TargetNetworkId,
                                     PointerScreenPosition,
                                     PointerWorldPosition,
                                     IsPrimaryButtonDown,
                                     IsSecondaryButtonDown);
                input.Set(data);
                _isInputChanged = false;
            }
        }

        #region Unity
        private void Update()
        {
            bool isPrimaryButtonDown = Input.GetMouseButton((int)InputButton.PrimaryButton);
            bool isSecondaryButtonDown = Input.GetMouseButton((int)InputButton.SecondaryButton);
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                if (PointerWorldPosition != hit.point)
                {
                    PointerWorldPosition = hit.point;
                }
                if ((!IsPrimaryButtonDown && isPrimaryButtonDown) ||
                    (!IsSecondaryButtonDown && isSecondaryButtonDown))
                {
                    NetworkObject networkObject = hit.transform.gameObject.GetComponentInParent<NetworkObject>();
                    NetworkId networkId = networkObject == null ? default : networkObject.Id;
                    if (isPrimaryButtonDown)
                    {
                        SelectedNetworkId = networkId;
                    }
                    if (isSecondaryButtonDown)
                    {
                        TargetNetworkId = networkId;
                    }
                }
                else if (IsSecondaryButtonDown && !isSecondaryButtonDown)
                {
                    TargetNetworkId = default;
                }
            }
            if (IsPrimaryButtonDown != isPrimaryButtonDown)
            {
                IsPrimaryButtonDown = isPrimaryButtonDown;
            }
            if (IsSecondaryButtonDown != isSecondaryButtonDown)
            {
                IsSecondaryButtonDown = isSecondaryButtonDown;
            }
            if (PointerScreenPosition != Input.mousePosition)
            {
                PointerScreenPosition = Input.mousePosition;
            }
        }
        #endregion
    }
}
