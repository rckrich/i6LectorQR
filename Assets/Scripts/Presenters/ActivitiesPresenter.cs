using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivitiesPresenter : Presenter
{
    private const string GET_ACTIVITIES = "https://i6yucatan.rckgames.com/api/activities/current/";
    private const string GET_ACTIVITIES_FINALIZADOS = "https://i6yucatan.rckgames.com/api/activities/finished/";
    public int offset = 0;
    public int limit = 20;
    public string type = "todas";
    public string date = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
    public bool finalizados = false;

    public override void CallInteractor(params object[] list)
    {
        if (!finalizados)
        {
            interactor.PerformSearch(GET_ACTIVITIES + type + "/" + date + "/" + offset + "/" + limit);
        }
        else
        {
            interactor.PerformSearch(GET_ACTIVITIES_FINALIZADOS + type + "/" + date + "/" + offset + "/" + limit);
        }
        
    }
}
