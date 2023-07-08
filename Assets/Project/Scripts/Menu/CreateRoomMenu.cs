using UnityEngine;
using NaughtyAttributes;

namespace Legends.UI
{
    public class CreateRoomMenu : Menu
    {
        public override void Awake()
        {
            base.Awake();
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
    }
}
