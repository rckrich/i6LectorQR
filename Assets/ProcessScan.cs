using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProcessScan : MonoBehaviour
{
    public TextMeshProUGUI name, email;
    public GameObject activityContainer, conferenceContainer, activityTitle, conferenceTitle, container;
    public GameObject textContentPrefab;

    private bool conferenceDone, activityDone;


    public void Initialize(string _result)
    {

        Debug.Log(_result);

        StartCoroutine(Yucatani6WebCalls.CR_User(_result, OnCallBack_Initialize_User));
        
    }

    public void OnCallBack_Initialize_User(object[] list)
    {
        UserRoot tempuser = (UserRoot)list[1];
        name.text = tempuser.user.name;
        email.text = tempuser.user.email;

        

    }


    
}
