using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Legends.Controllers{

    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Vector3 _posOffset;
        [SerializeField] private Vector3 _lookOffset;

        private Transform _target;

        private void LateUpdate()
        {
            if (_target != null)
            {
                transform.position = _target.position + _posOffset;
                transform.LookAt(_target.position + _lookOffset);
            }
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }

}
