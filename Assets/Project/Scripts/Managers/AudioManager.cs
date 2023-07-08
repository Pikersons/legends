using System.Collections;
using NaughtyAttributes;
using UnityEngine;

namespace Legends.Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        [field: ReadOnly]
        [field: SerializeField]
        public AudioSource BGMAudioSource { get; set; }

        #region Setup
        private void Awake()
        {
            GetDepenencies();
        }

        [Button("Setup")]
        public void GetDepenencies()
        {
            BGMAudioSource = GetComponent<AudioSource>();
        }
        #endregion

        public void PlaySFX(AudioClip clip, float delay = 0)
        {
            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            newAudioSource.clip = clip;
            newAudioSource.PlayDelayed(delay);
            DestroySFX(newAudioSource, clip.length);
        }

        public IEnumerator PlaySFXCO(AudioClip clip)
         {
            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            newAudioSource.clip = clip;
            newAudioSource.Play();
            yield return new WaitForSeconds(clip.length);
            Destroy(newAudioSource);
        }

        #region Auxiliar Methods
        private void DestroySFX(AudioSource audioSOurce, float delay)
        {
            StartCoroutine(DestroySFXCO(audioSOurce, delay));
        }

        private IEnumerator DestroySFXCO(AudioSource audioSource, float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(audioSource);
        }
        #endregion

    }

}
