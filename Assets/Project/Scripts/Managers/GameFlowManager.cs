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

        [Button("Setup Game Flow")]
        public void GetDependencies()
        {
            FadeController = FindObjectOfType<FadeController>();
            AudioManager = FindObjectOfType<AudioManager>();
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

            AudioManager.MuteBGM();
            AudioManager.StartBGM();
            AudioManager.FadeOutBGM(2f);
        }

        public void TransitionToCharacterSelection()
        {
            StartTransition();
        }

        private void StartTransition()
        {
            GameState = GameState.Transitioning;
            AudioManager.FadeInBGM(2f);
            FadeController.FadeIn(0.57f);
        }

        private void EndTransition()
        {
            AudioManager.FadeOutBGM(2f);
            FadeController.FadeOut(0.57f);
        }

    }

    
}
