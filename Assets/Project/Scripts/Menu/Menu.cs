using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using Legends.ScriptableObjects;

namespace Legends.UI
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class Menu : MonoBehaviour
    {
        [field: SerializeField] public AudioClip ShowSound { get; set; }
        [field: SerializeField] public AudioClip HideSound { get; set; }
        [field: SerializeField] public Vector2 ShowPosition { get; set; }
        [field: SerializeField] public Vector2 HidePosition { get; set; }

        #region Settings and Dependencies
        [field: Foldout("Menu Settings")]
        [field: SerializeField]
        [field: ReadOnly]
        public MenuSettings MenuSettings { get; set; }

        [field: Foldout("Menu Settings")]
        [field: SerializeField]
        [field: ReadOnly]
        protected RectTransform RectTransform { private get; set; }

        [field: Foldout("Menu Settings")]
        [field: SerializeField]
        [field: ReadOnly]
        public AudioSource AudioSource { get; set; }

        [field: Foldout("Menu Settings")]
        [field: SerializeField]
        [field: ReadOnly]
        public bool IsVisible { get; set; }

        [Button("Setup")]
        public void GetDependencies()
        {
            MenuSettings = Resources.Load("MenuSettings") as MenuSettings;
            RectTransform = GetComponent<RectTransform>();
            AudioSource = GetComponent<AudioSource>();
        }
        #endregion

        public virtual void Awake()
        {
            GetDependencies();
            IsVisible = false;
        }

        public virtual void Show()
        {
            if (IsVisible) return;

            AudioSource.clip = ShowSound;
            AudioSource.pitch = MenuSettings.showSoundPitch;
            AudioSource.Play();
            RectTransform
                .DOAnchorPos(ShowPosition, 1f, false)
                .SetEase(MenuSettings.easeType)
                .OnComplete( () => IsVisible = true );
        }

        public virtual void Hide()
        {
            if (!IsVisible) return;

            AudioSource.clip = HideSound;
            AudioSource.pitch = MenuSettings.hideSoundPitch;
            AudioSource.Play();
            RectTransform
                .DOAnchorPos(HidePosition, 1f, false)
                .SetEase(MenuSettings.easeType)
                .OnComplete( () => IsVisible = false );
        }

    }
}
