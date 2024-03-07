using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WebCallsUtils
{
    public static long SUCCESS_RESPONSE_CODE { get { return 200; } }
    public static long ERROR_RESPONSE_CODE { get { return 400; } }
    public static long AUTHORIZATION_FAILED_RESPONSE_CODE { get { return 401; } }
    public static long ITEM_NOT_AVAILABLE_RESPONSE_CODE { get { return 403; } }
    public static long NOT_FOUND_RESPONSE_CODE { get { return 404; } }
    public static long REQUEST_TIMEOUT_CODE { get { return 408; } }
    public static long TOO_MANY_REQUEST_CODE { get { return 429; } }
    public static long SERVICE_NOT_AVAILABLE_RESPONSE_CODE { get { return 503; } }
    public static long GATEWAY_TIMEOUT_CODE { get { return 504; } }

    public static string AddParametersToURI(string _uri, Dictionary<string, string> _parameters)
    {
        string url = _uri;

        foreach (KeyValuePair<string, string> kvp in _parameters)
        {
            url = url + kvp.Key + "=" + kvp.Value + "&";
        }

        url = url.TrimEnd('&');

        DebugLogManager.instance.DebugLog(("Complete url is: " + url));

        return url;
    }

    public static string AddMultipleParameterToUri(string _uri, string _key, string[] _parameters)
    {
        string url = _uri + _key + "=";

        foreach (string track_id in _parameters)
        {
            url = url + track_id + "%2C";
        }

        url = url.Remove(url.Length - 3);

        DebugLogManager.instance.DebugLog(("Complete url with new multiple param is: " + url));

        return url;
    }

    public static Texture2D GetTextureCopy(Texture2D _source)
    {
        RenderTexture rt = RenderTexture.GetTemporary(_source.width, _source.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
        Graphics.Blit(_source, rt);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = rt;
        Texture2D readableTexture = new Texture2D(_source.width, _source.height);
        readableTexture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        readableTexture.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(rt);

        return readableTexture;
    }

    public static bool CheckIfServerServiceIsAvailable(long _responseCode)
    {
        if (_responseCode.Equals(WebCallsUtils.SERVICE_NOT_AVAILABLE_RESPONSE_CODE))
        {
            NewScreenManager.instance.GetCurrentView().EndSearch();

            NewScreenManager.instance.ChangeToMainView(ViewID.PopUpViewModel, true);
            PopUpViewModel popUpViewModel = (PopUpViewModel)NewScreenManager.instance.GetMainView(ViewID.PopUpViewModel);
            popUpViewModel.Initialize(PopUpViewModelTypes.MessageOnly, "Servicio no disponible", "El servidor de Spotify no puede responder en estos momentos. Volver a intetnar en un rato.", "Aceptar");
            popUpViewModel.SetPopUpAction(() => { NewScreenManager.instance.BackToPreviousView(); });

            return true;
        }

        return false;
    }

    public static bool IsResponseAnyError(long _responseCode)
    {
        if (_responseCode.Equals(WebCallsUtils.ERROR_RESPONSE_CODE)) return true;
        if (_responseCode.Equals(WebCallsUtils.AUTHORIZATION_FAILED_RESPONSE_CODE)) return true;
        if (_responseCode.Equals(WebCallsUtils.ITEM_NOT_AVAILABLE_RESPONSE_CODE)) return true;
        if (_responseCode.Equals(WebCallsUtils.NOT_FOUND_RESPONSE_CODE)) return true;
        if (_responseCode.Equals(WebCallsUtils.SERVICE_NOT_AVAILABLE_RESPONSE_CODE)) return true;

        return false;
    }

    public static bool IsResponseItemNotFound(long _responseCode)
    {
        if (_responseCode.Equals(WebCallsUtils.ITEM_NOT_AVAILABLE_RESPONSE_CODE)) return true;
        if (_responseCode.Equals(WebCallsUtils.NOT_FOUND_RESPONSE_CODE)) return true;

        return false;
    }
}
