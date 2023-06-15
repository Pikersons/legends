using Fusion;
using Legends.Controllers;
using Legends.Core.Models;
using UnityEngine;

namespace Legends.Managers
{
    public class InputManager : MonoBehaviour
    {
        private Vector3 _destination;
        private PlayerRef _playerRef;
        private PlayerRef _targetPlayerRef;
        private bool _isLeftMouseDown;
        private bool _isRightMouseDown;

        public void PopulateInput(NetworkInput input)
        {
            InputData data = new(_destination, _targetPlayerRef, _isLeftMouseDown, _isRightMouseDown);
            input.Set(data);
        }

        #region Unity
        private void Update()
        {
            _isLeftMouseDown = Input.GetMouseButton(0);
            _isRightMouseDown = Input.GetMouseButton(1);
            if (_isLeftMouseDown)
            {
                
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
                {
                    _destination = hit.point;
                }
            }
            if (_isRightMouseDown)
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
                {
                    NetworkObject networkObject = hit.transform.gameObject.GetComponentInParent<NetworkObject>();
                    if (networkObject != null && networkObject.InputAuthority != _playerRef)
                    {
                        _targetPlayerRef = networkObject.InputAuthority;
                    }
                    else
                    {
                        _targetPlayerRef = PlayerRef.None;
                    }
                }
            }
        }
        #endregion

        public void SetPlayerRef(PlayerRef playerRef)
        {
            _playerRef = playerRef;
        }
    }
}
