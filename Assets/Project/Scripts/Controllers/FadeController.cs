using System.Collections;
using Legends.ScriptableObjects;
using NaughtyAttributes;
using UnityEngine;

namespace Legends.Controllers
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeController : MonoBehaviour
    {
        public float FadeTime { get; set; }
        [field : SerializeField] public CanvasGroup CanvasGroup { get; set; }

        private void Awake()
        {
            GetDependencies();
            if (FadeTime == 0)
            {
                FadeTime = 1f;
            }
        }

        [Button("Setup")]
        public void GetDependencies()
        {
            CanvasGroup = GetComponent<CanvasGroup>();
        }

        [Button("FadeIn")]
        public void FadeIn()
        {
            StartCoroutine(FadeInCo());
        }

        [Button("FadeOut")]
        public void FadeOut()
        {
            StartCoroutine(FadeOutCo());
        }

        private IEnumerator FadeInCo()
        {
            float start = CanvasGroup.alpha;
            float end = 1f;
            float velocity = (end - start) / FadeTime; //vm = ds/dt

            CanvasGroup.blocksRaycasts = true;
            while (CanvasGroup.alpha < end)
            {
                CanvasGroup.alpha += velocity * Time.deltaTime;
                yield return null;
            }
            CanvasGroup.alpha = end;
        }

        private IEnumerator FadeOutCo()
        {
            float start = CanvasGroup.alpha;
            float end = 0f;
            float velocity = (end - start) / FadeTime; //vm = ds/dt

            while (CanvasGroup.alpha > end)
            {
                CanvasGroup.alpha += velocity * Time.deltaTime;
                yield return null;
            }
            CanvasGroup.alpha = end;
            CanvasGroup.blocksRaycasts = false;
        }
    }
}
