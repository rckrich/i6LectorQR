using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaderDemoViewModel : ViewModel
{
    public void OnClick_Settings()
    {
        NewScreenManager.instance.ChangeToMainView(ViewID.SettingsDemoViewModel, true);
    }
}
