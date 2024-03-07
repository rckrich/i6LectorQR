using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoFilterAppEvent : AppEvent
{
    public bool filteron;

    public NoFilterAppEvent(bool _filteron):base (_filteron)
    {
        filteron = _filteron;
    }
}
