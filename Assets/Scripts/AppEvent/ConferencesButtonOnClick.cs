using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConferencesButtonOnClick : AppEvent
{
    private Conference conference;

    public ConferencesButtonOnClick(Conference _conference) : base(_conference)
    {
        conference = _conference;
    }

    public Conference GetSelectedConferences() { return conference;  }
}
