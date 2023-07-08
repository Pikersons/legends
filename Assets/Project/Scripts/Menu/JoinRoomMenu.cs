using UnityEngine;
using NaughtyAttributes;

namespace Legends.UI
{
    public class JoinRoomMenu : Menu
    {
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
