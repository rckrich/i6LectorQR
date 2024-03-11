using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProcessScan : MonoBehaviour
{
    public TextMeshProUGUI nametxt, email;
    public GameObject activityContainer, conferenceContainer, activityTitle, conferenceTitle, container;
    public GameObject textContentPrefab;

    private List<GameObject> TextList = new List<GameObject>();

    private void OnEnable() {
        Debug.Log(2);
    }

    public void InitializeScan(UserRoot tempuser)
    {
        activityContainer.SetActive(false);
        conferenceContainer.SetActive(false);

        Canvas.ForceUpdateCanvases();
        
        nametxt.text = tempuser.user.name;
        email.text = tempuser.user.email;

        if(tempuser.user.activities != null)
        {
            activityContainer.SetActive(true);
            activityTitle.SetActive(true);
            foreach (Activity item in tempuser.user.activities)
            {
                GameObject Instance = Instantiate(textContentPrefab, activityContainer.transform);
                Instance.GetComponent<TextMeshProUGUI>().text = item.title;
                TextList.Add(Instance);
            }
        }
        else
        {
            activityContainer.SetActive(false);
            activityTitle.SetActive(false);
        }

        if (tempuser.user.conferences != null)
        {
            conferenceContainer.SetActive(true);
            conferenceTitle.SetActive(true);
            foreach (Conference item in tempuser.user.conferences)
            {
                GameObject Instance = Instantiate(textContentPrefab, conferenceContainer.transform);
                Instance.GetComponent<TextMeshProUGUI>().text = item.title;
                TextList.Add(Instance);
            }
        }
        else
        {
            conferenceContainer.SetActive(false);
            conferenceTitle.SetActive(false);
        }
        
    }
    private void ClearMessage()
    {
        nametxt.text = " ";
        email.text = " ";
        if(TextList.Count > 0)
        {
            foreach (GameObject item in TextList)
            {
                Destroy(item);
            }
            TextList.Clear();
        }

        conferenceContainer.SetActive(false);
        conferenceTitle.SetActive(false);
        activityContainer.SetActive(false);
        activityTitle.SetActive(false);

    }


    public void OnClick_Acept()
    {
        ClearMessage();

    }

    
}
