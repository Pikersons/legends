using Fusion;
using Legends.Core.Models;
using UnityEngine;

namespace Legends.Managers
{
    public class InputManager : MonoBehaviour
    {
        private Vector3 _pointerScreenPosition;
        private Vector3 _pointerWorldPosition;
        private bool _isInputChanged;
        private bool _isPrimaryButtonDown;
        private bool _isSecondaryButtonDown;

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
                InputData data = new(PointerScreenPosition,
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
            if (isPrimaryButtonDown != IsPrimaryButtonDown)
            {
                IsPrimaryButtonDown = isPrimaryButtonDown;
            }
            if (isSecondaryButtonDown != IsSecondaryButtonDown)
            {
                IsSecondaryButtonDown = isSecondaryButtonDown;
            }
            if (Input.mousePosition != PointerScreenPosition)
            {
                PointerScreenPosition = Input.mousePosition;
            }
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                if (hit.point != PointerWorldPosition)
                {
                    PointerWorldPosition = hit.point;
                }
            }
        }
        #endregion
    }
}
