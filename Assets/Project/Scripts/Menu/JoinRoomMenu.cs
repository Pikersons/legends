using UnityEngine;
using NaughtyAttributes;
using Legends.Controllers;
using Legends.Managers;
using Legends.ScriptableObjects;

namespace Legends.UI
{
    public class JoinRoomMenu : Menu
    {
        #region Dependencies and Setup

        [field: SerializeField]
        public GameNetworkData GameNetworkData { get; set; }

        [Button("Setup Menu CreateRoom")]
        public new void GetDependencies()
        {
            GameNetworkData = Resources.Load<GameNetworkData>("GameNetworkData");
        }

        public override void Awake()
        {
            base.Awake();
            GetDependencies();
        }

        #endregion

        public void OnClickJoinRoom()
        {
            //AudioManager.PlaySFX(CreateRoomClickSound);
            GameNetworkData.gameMode = Fusion.GameMode.Client;
            //GameFlowManager.TransitionToCharacterSelection();
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
