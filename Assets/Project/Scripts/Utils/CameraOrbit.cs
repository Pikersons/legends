using UnityEngine;

namespace Legends
{
    public class CameraOrbit : MonoBehaviour
    {
        [field: SerializeField] public float Speed { get; private set; }

        private void LateUpdate()
        {
            transform.Rotate(new Vector3(0, Speed * Time.deltaTime, 0));
        }
    }
}
