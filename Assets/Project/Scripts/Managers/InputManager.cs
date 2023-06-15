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

        public void PopulateInput(NetworkInput input)
        {
            InputData data = new(_destination, _targetPlayerRef);
            input.Set(data);
        }

        #region Unity
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
                {
                    _destination = hit.point;
                }
            }

            if (Input.GetMouseButton(1))
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
