using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConferencesPresenter : Presenter
{

    private const string GET_CONFERENCES = "https://i6yucatan.rckgames.com/api/conferences/current/";
    private const string GET_CONFERENCES_FINALIZADOS = "https://i6yucatan.rckgames.com/api/conferences/finished/";
    public int offset = 0;
    public int limit = 20;
    public string type = "todas";
    public string date = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
    public bool finalizados = false;

    public override void CallInteractor(params object[] list)
    {
        if (!finalizados)
        {
            interactor.PerformSearch(GET_CONFERENCES + type + "/" + date + "/" + offset + "/" + limit);
        }
        else
        {
            interactor.PerformSearch(GET_CONFERENCES_FINALIZADOS + type + "/" + date + "/" + offset + "/" + limit);
        }
        }
}
