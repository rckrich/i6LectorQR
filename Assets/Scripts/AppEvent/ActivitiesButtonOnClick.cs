using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivitiesButtonOnClick : AppEvent
{
    private Activity activity;

    public ActivitiesButtonOnClick(Activity _activity) : base(_activity)
    {
        activity = _activity;
    }

    public Activity GetSelectedActivity() { return activity; }
}
