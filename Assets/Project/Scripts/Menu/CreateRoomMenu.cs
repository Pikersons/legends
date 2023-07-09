using UnityEngine;
using NaughtyAttributes;
using Legends.Controllers;
using Legends.Managers;
using UnityEngine.Events;
using Legends.ScriptableObjects;

namespace Legends.UI
{
    public class CreateRoomMenu : Menu
    {
        #region Dependencies and Setup

        [field: SerializeField]
        public GameFlowManager GameFlowManager { get; set; }

        [field: SerializeField]
        public FadeController FadeController { get; set; }

        [field: SerializeField]
        public AudioManager AudioManager { get; set; }

        [field: SerializeField]
        public AudioClip CreateRoomClickSound { get; set; }

        [field: SerializeField]
        public GameNetworkData GameNetworkData { get; set; }

        [Button("Setup Menu CreateRoom")]
        public new void GetDependencies()
        {
            GameFlowManager = FindObjectOfType<GameFlowManager>();
            FadeController = FindObjectOfType<FadeController>();
            AudioManager = FindObjectOfType<AudioManager>();
            GameNetworkData = Resources.Load<GameNetworkData>("GameNetworkData");
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
            GameNetworkData.gameMode = Fusion.GameMode.Host;
            GameFlowManager.TransitionToCharacterSelection();
        }

        public void Input_OnValueChanged(string roomName)
        {
            GameNetworkData.roomName = roomName;
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