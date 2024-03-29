using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Presenter : AppObject
{
    protected ViewModel viewModel;

    protected Interactor interactor;

    public virtual void Initialize<TV, TP, TI>(TV _viewModel, TP _presenter) where TV : ViewModel where TP : Presenter where TI : Interactor {
        this.viewModel = _viewModel;
        interactor = gameObject.AddComponent(typeof(TI)) as TI;
        interactor.Initialize(_presenter);
    }

    public virtual void CallInteractor(params object[] list) { }

    public virtual void OnResult(params object[] list) { viewModel.DisplayOnResult(list); }

    public virtual void OnFailedResult(params object[] list)
    {
        if(list != null)
        {
            viewModel.EndSearch("Error", (string)list[0]);
        }
        else
        {
            OnErrorMessage();
        }
        
    }

    public virtual void OnNetworkError() {
        OnErrorMessage();
        viewModel.DisplayOnNetworkError();
    }

    public virtual void OnServerError()
    {
        OnErrorMessage();
        viewModel.DisplayOnServerError();
    }

    protected virtual void CallPopUP(PopUpViewModelTypes _type, string _tileText, string _descriptionText, string _actionButtonText = "")
    {
        NewScreenManager.instance.ChangeToMainView(ViewID.PopUpViewModel, true);
        PopUpViewModel popUpViewModel = (PopUpViewModel)NewScreenManager.instance.GetMainView(ViewID.PopUpViewModel);
        popUpViewModel.Initialize(_type, _tileText, _descriptionText, _actionButtonText);
        popUpViewModel.SetPopUpAction(() => { NewScreenManager.instance.BackToPreviousView(); });
    }

    protected virtual void OnErrorMessage()
    {
        viewModel.EndSearch("Error de conexión", "Ha ocurrido un problema, por favor revisa que todo este bien y vuelve a intentarlo");
    }
}