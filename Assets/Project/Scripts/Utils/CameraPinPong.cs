using UnityEngine;
using DG.Tweening;

namespace Legends.Utils
{
    public class CameraPinPong : MonoBehaviour
    {
        [field: SerializeField] public Vector3 EndPoint { get; set; }
        [field: SerializeField] public Ease EaseType { get; set; }
        [field: SerializeField] public float TotalTime { get; set; }

        void Start()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.SetDelay(0f);
            sequence.Append(

                transform
                    .DOMove(EndPoint, TotalTime)
                    .SetEase(EaseType));

            sequence.AppendInterval(0f);
            sequence.SetLoops(-1, LoopType.Yoyo);
            sequence.Play();
        }
    }
}
