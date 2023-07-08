using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Legends.Controllers
{
    public class MainMenuCameraController : MonoBehaviour
    {
        #region Dependencies and Setup
        [field: SerializeField]
        public Camera [] Cameras { get; set; }

        [field: ReadOnly]
        [field: SerializeField]
        public int CurrentIndex { get; set; }

        [field: SerializeField]
        [field: MinMaxSlider(3f, 48f)]
        public Vector2 TimeInterval { get; set; }
        #endregion

        private void Start()
        {
            CurrentIndex = 0;
            StartCoroutine(ChangeCamerasOverTime());
        }

        private IEnumerator ChangeCamerasOverTime()
        {
            while (true)
            {
                EnableSingleCamera(CurrentIndex);
                yield return new WaitForSeconds(Random.Range(TimeInterval.x, TimeInterval.y));
                CurrentIndex = (CurrentIndex + 1) % Cameras.Length;
            }
        }

        private void EnableSingleCamera(int index) {
            int size = Cameras.Length;
            for (int i = 0; i < size; i++)
            {
                if (i != index)
                {
                    Cameras[i].gameObject.SetActive(false);
                    continue;
                }
                Cameras[i].gameObject.SetActive(true);
            }
        }
    }

}
