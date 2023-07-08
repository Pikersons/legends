using System.Collections;
using NaughtyAttributes;
using UnityEngine;

namespace Legends.Controllers
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeController : MonoBehaviour
    {
        #region Dependencies and Setup
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
        #endregion

        [Button("FadeIn")]
        public void FadeIn(float delay = 0)
        {
            StartCoroutine(FadeInCo(delay));
        }

        [Button("FadeOut")]
        public void FadeOut(float delay = 0)
        {
            StartCoroutine(FadeOutCo(delay));
        }

        #region Auxiliar Methods
        private IEnumerator FadeInCo(float delay = 0)
        {
            float start = CanvasGroup.alpha;
            float end = 1f;
            float velocity = (end - start) / FadeTime; //vm = ds/dt

            yield return new WaitForSeconds(delay);

            CanvasGroup.blocksRaycasts = true;
            while (CanvasGroup.alpha < end)
            {
                CanvasGroup.alpha += velocity * Time.deltaTime;
                yield return null;
            }
            CanvasGroup.alpha = end;
        }

        private IEnumerator FadeOutCo(float delay = 0)
        {
            float start = CanvasGroup.alpha;
            float end = 0f;
            float velocity = (end - start) / FadeTime; //vm = ds/dt

            yield return new WaitForSeconds(delay);

            while (CanvasGroup.alpha > end)
            {
                CanvasGroup.alpha += velocity * Time.deltaTime;
                yield return null;
            }
            CanvasGroup.alpha = end;
            CanvasGroup.blocksRaycasts = false;
        }
        #endregion
    }
}
