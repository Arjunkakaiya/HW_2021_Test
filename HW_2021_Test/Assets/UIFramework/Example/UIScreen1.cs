using UnityEngine;

namespace Arjun
{
    public class UIScreen1: ScreenView
    {
        public ScreenType nextScreenType;
        public ScreenType myScreenType;
        public PopupType nextPopupType;

        public void OnNextScreen()
        {
            UIController.instance.ShowThisScreen(nextScreenType, EnableDirection.Forward);
            UIController.instance.HideThisScreen(myScreenType, EnableDirection.Reverse);
        }
    }
}
