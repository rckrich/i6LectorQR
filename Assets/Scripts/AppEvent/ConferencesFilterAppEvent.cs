using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConferencesFilterAppEvent : AppEvent
{
    public string filtername;

    public ConferencesFilterAppEvent(string _filtername) : base(_filtername)
    {
        filtername = _filtername;
    }
}
