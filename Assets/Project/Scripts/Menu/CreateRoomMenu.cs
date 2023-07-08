using UnityEngine;
using NaughtyAttributes;
using Legends.Controllers;
using Legends.Managers;
using System.Collections;

namespace Legends.UI
{
    public class CreateRoomMenu : Menu
    {
        #region CreateRoom
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
        #endregion

        public override void Awake()
        {
            base.Awake();
            GetDependencies();
            Show();
        }

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

        public void OnClickCreateRoom()
        {
            AudioManager.PlaySFX(CreateRoomClickSound);
            FadeController.FadeIn(0.57f);
            //StartCoroutine(OnClickCreateRoomCO());
        }

        public IEnumerator OnClickCreateRoomCO()
        {
            yield return AudioManager.PlaySFXCO(CreateRoomClickSound);
            FadeController.FadeIn();
            
        }
    }
}