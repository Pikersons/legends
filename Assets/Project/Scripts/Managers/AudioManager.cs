using System.Collections;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

namespace Legends.Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        #region Setup
        [field: ReadOnly]
        [field: SerializeField]
        public AudioSource BGMAudioSource { get; set; }

        [field: ReadOnly]
        [field: SerializeField]
        public AudioMixer AudioMixer { get; set; }

        [Button("Setup")]
        public void GetDepenencies()
        {
            BGMAudioSource = GetComponent<AudioSource>();
            AudioMixer = Resources.Load<AudioMixer>("AudioMixer");
        }

        private void Awake()
        {
            GetDepenencies();
        }
        #endregion

        public void StartBGM()
        {
            StartCoroutine(PlayDelayedCO(BGMAudioSource, 2f));
        }

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

        public void MuteBGM()
        {
            AudioMixer.SetFloat("BGM", -80);
        }

        public void UnMuteBGM()
        {
            AudioMixer.SetFloat("BGM", 0);
        }

        public void FadeInBGM(float totalTime = 1f)
        {
            StartCoroutine(FadeBGMCO(-80f, totalTime));
        }

        public void FadeOutBGM(float totalTime = 1f)
        {
            StartCoroutine(FadeBGMCO(0f, totalTime));
        }

        #region Auxiliar Methods
        private IEnumerator PlayDelayedCO(AudioSource audioSource, float delay)
        {
            audioSource.PlayDelayed(delay);
            yield return null;
        }

        private void DestroySFX(AudioSource audioSOurce, float delay)
        {
            StartCoroutine(DestroySFXCO(audioSOurce, delay));
        }

        private IEnumerator DestroySFXCO(AudioSource audioSource, float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(audioSource);
        }

        private IEnumerator FadeBGMCO(float endValue, float totalTime = 1f)
        {
            
            AudioMixer.GetFloat("BGM", out float start);
            float end = endValue;
            float lerpTime = 0f;

            while (lerpTime < totalTime)
            {
                lerpTime += Time.deltaTime;
                AudioMixer.SetFloat("BGM", Mathf.Lerp(start, end, lerpTime / totalTime));
                yield return null;
            }

            AudioMixer.SetFloat("BGM", end);
        }

        private IEnumerator LerpVolume(AudioSource audioSource, float finalVolume, float totalTime = 1f)
        {
            float start = audioSource.volume;
            float end = finalVolume;
            float lerpTime = 0f;

            while (lerpTime < totalTime)
            {
                lerpTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, end, lerpTime / totalTime);
                yield return null;
            }

            audioSource.volume = finalVolume;
        }
        #endregion

    }

}
