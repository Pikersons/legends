using UnityEngine;

namespace Legends
{
    public class CreateRoomMenu : Menu
    {
        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }

        [ContextMenu("Hide")]
        public void TestHide() {
            Hide();
        }

        [ContextMenu("Show")]
        public void TestShow()
        {
            Show();
        }

    }
}
