using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;
using UnityEngine.XR;

public class Yucatani6WebCalls : WebCalls
{
    public static IEnumerator CR_User(string _pass, WebCallback _callback, int _offset = 0, int _limit = 20)
    {
        string jsonResult = "";

        string localURL = "https://i6yucatan.rckgames.com/api/v1/users/pass/" + _pass;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(localURL))
        {
            webRequest.SetRequestHeader("Accept", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                //Catch response code for multiple requests to the server in a short timespan.

                if (webRequest.responseCode.Equals(WebCallsUtils.AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    //TODO Response when unauthorized
                }

                DebugLogManager.instance.DebugLog("Protocol Error or Connection Error on fetch profile. Response Code: " + webRequest.responseCode + ". Result: " + webRequest.result.ToString());
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    jsonResult = webRequest.downloadHandler.text;
                    Debug.Log("Fetch conference status result: " + jsonResult);
                    JsonSerializerSettings settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
                    UserRoot userRoot = JsonConvert.DeserializeObject<UserRoot>(jsonResult, settings);
                    _callback(new object[] { webRequest.responseCode, userRoot });
                    yield break;
                }
            }

            DebugLogManager.instance.DebugLog("Failed fetch conference status result: " + jsonResult);
            yield break;
        }
    }
    public static IEnumerator CR_GetSeveralRegisteredActivities(string _user_id, WebCallback _callback, int _offset = 0, int _limit = 20)
    {
        string jsonResult = "";

        string url = isNotTest ? endpointStartURI : testEndpointStartURI;
        url += "/v1/auth/" + _user_id + "/activities/" + _limit + "/" + _offset;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", ProgressManager.instance.progress.userDataPersistance.token_type + " " + ProgressManager.instance.progress.userDataPersistance.access_token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                //Catch response code for multiple requests to the server in a short timespan.

                if (webRequest.responseCode.Equals(WebCallsUtils.AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    //TODO Response when unauthorized
                }

                DebugLogManager.instance.DebugLog("Protocol Error or Connection Error on fetch profile. Response Code: " + webRequest.responseCode + ". Result: " + webRequest.result.ToString());
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    jsonResult = webRequest.downloadHandler.text;
                    DebugLogManager.instance.DebugLog("Fetch several registered activities result: " + jsonResult);
                    JsonSerializerSettings settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
                    GetSeveralActivities getSeveralActivities = JsonConvert.DeserializeObject<GetSeveralActivities>(jsonResult, settings);
                    _callback(new object[] { webRequest.responseCode, getSeveralActivities });
                    yield break;
                }
            }

            DebugLogManager.instance.DebugLog("Failed fetch several registered activities result: " + jsonResult);
            yield break;
        }
    }

    public static IEnumerator CR_GetSeveralRegisteredConferences(string _user_id, WebCallback _callback, int _offset = 0, int _limit = 20)
    {
        string jsonResult = "";

        string url = isNotTest ? endpointStartURI : testEndpointStartURI;
        url += "/v1/auth/" + _user_id + "/conferences/" + _limit + "/" + _offset;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", ProgressManager.instance.progress.userDataPersistance.token_type + " " + ProgressManager.instance.progress.userDataPersistance.access_token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                //Catch response code for multiple requests to the server in a short timespan.

                if (webRequest.responseCode.Equals(WebCallsUtils.AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    //TODO Response when unauthorized
                }

                DebugLogManager.instance.DebugLog("Protocol Error or Connection Error on fetch profile. Response Code: " + webRequest.responseCode + ". Result: " + webRequest.result.ToString());
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    jsonResult = webRequest.downloadHandler.text;
                    DebugLogManager.instance.DebugLog("Fetch several registered conferences result: " + jsonResult);
                    JsonSerializerSettings settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
                    GetSeveralConference getSeveralConference = JsonConvert.DeserializeObject<GetSeveralConference>(jsonResult, settings);
                    _callback(new object[] { webRequest.responseCode, getSeveralConference });
                    yield break;
                }
            }

            DebugLogManager.instance.DebugLog("Failed fetch several registered conferences result: " + jsonResult);
            yield break;
        }
    }


}