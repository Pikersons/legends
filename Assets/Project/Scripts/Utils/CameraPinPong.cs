using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

namespace Legends.Utils
{

    public enum CameraPinPongMovementType
    {
        Position,
        Rotation
    }

    public class CameraPinPong : MonoBehaviour
    {
        #region Setup and Dependencies
        [field: SerializeField] public CameraPinPongMovementType MovementType { get; set; }
        [field: SerializeField] public Vector3 EndPoint { get; set; }
        [field: SerializeField] public Ease EaseType { get; set; }
        [field: SerializeField] public float TotalTime { get; set; }
        [field: ReadOnly][field: SerializeField] public Sequence Sequence { get; set; }

        private void Awake()
        {
            Sequence = DOTween.Sequence();
            Sequence.SetDelay(0f);
            Sequence.Append( GeneratedTween() );
            Sequence.AppendInterval(0f);
            Sequence.SetLoops(-1, LoopType.Yoyo);
        }
        #endregion

        void Start()
        {
            Sequence.Play();
        }

        private Tween GeneratedTween()
        {
            Tween tween = null;

            switch (MovementType)
            {
                case CameraPinPongMovementType.Position:
                    tween = transform
                                .DOMove(EndPoint, TotalTime)
                                .SetEase(EaseType);
                    break;
                case CameraPinPongMovementType.Rotation:
                    tween = transform
                                .DORotate(EndPoint, TotalTime)
                                .SetEase(EaseType);
                    break;
            }

            return tween;
        }
    }
}
