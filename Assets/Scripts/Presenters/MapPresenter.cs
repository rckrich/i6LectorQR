using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPresenter : Presenter
{
    private const string GET_MAP = "https://i6yucatan.rckgames.com/api/configurations/image";

    public override void CallInteractor(params object[] list)
    {
        interactor.PerformSearch(GET_MAP);
    }
}
