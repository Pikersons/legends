using UnityEngine;
using DG.Tweening;

namespace Legends
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class Menu : MonoBehaviour
    {
        [field: SerializeField] protected RectTransform RectTransform { private get; set; }
        [field: SerializeField] public AudioSource AudioSource { get; set; }
        [field: SerializeField] public AudioClip ShowSound { get; set; }
        [field: SerializeField] public AudioClip HideSound { get; set; }
        [field: SerializeField] public bool IsVisible { get; set; }

        private const float ShowPitch = 1.0f;
        private const float HidePitch = 1.7f;
        

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            AudioSource = GetComponent<AudioSource>();
            IsVisible = false;
        }

        public virtual void Show()
        {
            AudioSource.clip = ShowSound;
            AudioSource.pitch = ShowPitch;
            AudioSource.Play();
            RectTransform.DOAnchorPos(new Vector2(0, 0f), 1f, false).SetEase(Ease.OutElastic);
        }

        public virtual void Hide() {
            AudioSource.clip = HideSound;
            AudioSource.pitch = HidePitch;
            AudioSource.Play();
            RectTransform.DOAnchorPos(new Vector2(-1000f, 0f), 1f, false).SetEase(Ease.OutElastic);
        }
        
    }
}
