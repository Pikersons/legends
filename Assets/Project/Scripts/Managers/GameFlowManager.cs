using Legends.Controllers;
using NaughtyAttributes;
using UnityEngine;

namespace Legends.Managers
{
    public enum GameState
    {
        Starting,
        MainMenu,
        Transitioning,
        InGame
    }

    public class GameFlowManager : MonoBehaviour
    {
        [field: ReadOnly]
        [field: SerializeField]
        public GameState GameState { get; set; }

        #region Dependencies and Setup
        [field: SerializeField]
        public FadeController FadeController { get; set; }

        [field: SerializeField]
        public AudioManager AudioManager { get; set; }

        [field: SerializeField]
        public NetworkManager NetworkManager { get; set; }

        [Button("Setup Game Flow")]
        public void GetDependencies()
        {
            FadeController = FindObjectOfType<FadeController>();
            AudioManager = FindObjectOfType<AudioManager>();
            NetworkManager = FindObjectOfType<NetworkManager>();
        }

        private void Awake()
        {
            GetDependencies();
        }
        #endregion

        private void Start()
        {
            GameState = GameState.Starting;
            FadeController.FadeInInstant();
            FadeController.FadeOut(1f);

            AudioManager.Mute(AudioManager.MixerGroup.BGM);
            AudioManager.StartBGM();
            AudioManager.FadeIn(AudioManager.MixerGroup.BGM, 3f);

            AudioManager.Mute(AudioManager.MixerGroup.Environment);
            AudioManager.FadeIn(AudioManager.MixerGroup.Environment, 4f);
        }

        public void TransitionToCharacterSelection()
        {
            StartTransition();
            NetworkManager.StartGame();
        }

        private void StartTransition()
        {
            GameState = GameState.Transitioning;
            AudioManager.FadeOut(AudioManager.MixerGroup.BGM, 2f);
            //AudioManager.FadeOut(AudioManager.MixerGroup.Environment, 2f);
            FadeController.FadeIn(0.57f);
        }

        private void EndTransition()
        {
            AudioManager.FadeIn(AudioManager.MixerGroup.BGM, 2f);
            //AudioManager.FadeIn(AudioManager.MixerGroup.Environment, 2f);
            FadeController.FadeOut(0.57f);
        }

    }

    
}
