using UnityEngine;
namespace Arjun
{
    public class PopupView : ScreenView
    {
        public void OnPopupClose()
        {
            PopupController.instance.CloseLastOpened();
        }
    }
}