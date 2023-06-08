using Fusion;
using Legends.Core.Models;
using UnityEngine;

namespace Legends.Managers
{
    public class InputManager : MonoBehaviour
    {
        private Vector3 _destination;

        public void PopulateInput(NetworkInput input)
        {
            InputData data = new(_destination);
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
        }
        #endregion
    }
}
