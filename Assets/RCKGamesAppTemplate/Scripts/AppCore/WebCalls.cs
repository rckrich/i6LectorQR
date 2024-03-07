using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;
using UnityEngine.XR;

public delegate void WebCallback(object[] _value);

public class WebCalls : MonoBehaviour
{
    public static bool isNotTest = true;
    public static string testEndpointStartURI = "";
    public static string endpointStartURI = "https://i6yucatan.rckgames.com/api";
}
