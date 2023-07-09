using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using Legends.ScriptableObjects;

namespace Legends.UI
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class Menu : MonoBehaviour
    {
        #region Menu
        [field: Foldout("Menu")]
        [field: SerializeField]
        public AudioClip ShowSound { get; set; }

        [field: Foldout("Menu")]
        [field: SerializeField]
        public AudioClip HideSound { get; set; }

        [field: Foldout("Menu")]
        [field: SerializeField]
        public Vector2 ShowPosition { get; set; }

        [field: Foldout("Menu")]
        [field: SerializeField]
        public Vector2 HidePosition { get; set; }

        [field: Foldout("Menu")]
        [field: SerializeField]
        [field: ReadOnly]
        public AudioSource AudioSource { get; set; }

        [field: Foldout("Menu")]
        [field: SerializeField]
        [field: ReadOnly]
        public bool IsVisible { get; set; }

        [field: Foldout("Menu")]
        [field: SerializeField]
        [field: ReadOnly]
        public MenuSettings MenuSettings { get; set; }

        [field: Foldout("Menu")]
        [field: SerializeField]
        [field: ReadOnly]
        protected RectTransform RectTransform { private get; set; }


        #endregion

        #region Setup and Dependencies
        [Button("Setup Menu")]
        public void GetDependencies()
        {
            MenuSettings = Resources.Load("MenuSettings") as MenuSettings;
            RectTransform = GetComponent<RectTransform>();
            AudioSource = GetComponent<AudioSource>();
        }

        public virtual void Awake()
        {
            GetDependencies();
            IsVisible = false;
        }
        #endregion

        public virtual void Show()
        {
            if (IsVisible) return;

            IsVisible = true;
            AudioSource.clip = ShowSound;
            AudioSource.Play();
            RectTransform
                .DOAnchorPos(ShowPosition, 1f, false)
                .SetEase(MenuSettings.easeType)
                .OnComplete( () => { } );
        }

        public virtual void Hide()
        {
            if (!IsVisible) return;

            IsVisible = false;
            RectTransform
                .DOAnchorPos(HidePosition, 1f, false)
                .SetEase(MenuSettings.easeType)
                .OnComplete( () => { } );
        }

    }
}
