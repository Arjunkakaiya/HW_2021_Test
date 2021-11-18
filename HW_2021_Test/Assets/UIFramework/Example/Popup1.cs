using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arjun
{
    public class Popup1 : PopupView
    {
        public PopupType myPopType;

        public void OnClose()
        {
            PopupController.instance.ClosePopupScreen(myPopType);
        }
    }
}
