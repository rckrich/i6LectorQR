using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConferencesNoFilterAppEvent : AppEvent
{
    public bool filteron;

    public ConferencesNoFilterAppEvent(bool _filteron) : base(_filteron)
    {
        filteron = _filteron;
    }
}
