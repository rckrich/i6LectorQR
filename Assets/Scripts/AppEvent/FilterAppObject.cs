using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class FilterAppObject : AppEvent
{
    public string filtername;

    public FilterAppObject(string _filtername) : base(_filtername)
    {
        filtername = _filtername;
    }
}
