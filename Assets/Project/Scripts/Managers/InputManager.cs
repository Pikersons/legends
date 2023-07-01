using Fusion;
using Legends.Core.Models;
using UnityEngine;

namespace Legends.Managers
{
    public class InputManager : MonoBehaviour
    {
        private Vector3 _pointerPosition;
        private bool _isInputChanged;
        private bool _isPrimaryButtonDown;
        private bool _isSecondaryButtonDown;

        public Vector3 PointerPosition
        {
            get => _pointerPosition;
            set
            {
                _pointerPosition = value;
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
                Debug.Log($"InputManager - {IsPrimaryButtonDown} - {IsSecondaryButtonDown} - {PointerPosition}");

                InputData data = new(PointerPosition,
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
            if (isPrimaryButtonDown != IsPrimaryButtonDown)
            {
                IsPrimaryButtonDown = isPrimaryButtonDown;
            }
            if (isSecondaryButtonDown != IsSecondaryButtonDown)
            {
                IsSecondaryButtonDown = isSecondaryButtonDown;
            }
            if (Input.mousePosition != PointerPosition)
            {
                PointerPosition = Input.mousePosition;
            }
        }
        #endregion
    }
}
