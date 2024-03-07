using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class MapInteractor : Interactor
{
    public override void PerformSearch(params object[] _list)
    {
        this._query = (string)_list[0];
        StartCoroutine(CR_GetMap(this._query));
    }

    private IEnumerator CR_GetMap(string uri, string[] parameters = null)
    {
        string jsonResult = "";

        string url = uri;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Accept", "application/json");
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                tryCatchServerError(webRequest.responseCode);
                presenter.OnServerError();
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    if (ErrorRequestManager.AnaliceResponseCode(webRequest.responseCode))
                    {
                        jsonResult = webRequest.downloadHandler.text;
                        
                        MapRoot mapRoot = JsonConvert.DeserializeObject<MapRoot>(jsonResult);
                        presenter.OnResult(mapRoot);
                        yield break;
                    }
                }
            }

            jsonResult = webRequest.downloadHandler.text;
            string error = jsonResult.ToString();
            presenter.OnFailedResult(null);
            yield break;

        }
    }
}
