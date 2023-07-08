using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Legends
{
    public class PersistentObject : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
