using UnityEngine;
using NaughtyAttributes;
using Legends.Controllers;
using Legends.Managers;

namespace Legends.UI
{
    public class CreateRoomMenu : Menu
    {
        #region Dependencies and Setup
        [field: Foldout("Create Room")]
        [field: SerializeField]
        public FadeController FadeController { get; set; }

        [field: Foldout("Create Room")]
        [field: SerializeField]
        public AudioManager AudioManager { get; set; }

        [field: Foldout("Create Room")]
        [field: SerializeField]
        public AudioClip CreateRoomClickSound { get; set; }

        [Button("Setup Menu CreateRoom")]
        public new void GetDependencies()
        {
            FadeController = FindObjectOfType<FadeController>();
            AudioManager = FindObjectOfType<AudioManager>();
        }

        public override void Awake()
        {
            base.Awake();
            GetDependencies();
        }
        #endregion

        private void Start()
        {
            Show();
        }

        public void OnClickCreateRoom()
        {
            AudioManager.PlaySFX(CreateRoomClickSound);
            FadeController.FadeIn(0.57f);
        }

        #region Auxiliar Methods
        [Button("Show")]
        public override void Show()
        {
            base.Show();
        }

        [Button("Hide")]
        public override void Hide()
        {
            base.Hide();
        }
        #endregion
    }
}