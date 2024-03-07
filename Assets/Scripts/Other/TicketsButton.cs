using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TicketsButton : ScrollViewModel, IImageDownloaderObject
{
    public TextMeshProUGUI nameTxt, typeTxt, dateTxt, hourTxt;
    public Image image;
    private Texture2D imageTexture;

    private Conference conference;
    private Activity activities;

    public void Initialize(Conference _conference = null, Activity _activities = null)
    {
        

        if(_activities != null)
        {
            activities = _activities;

            activities.title = nameTxt.text;
            activities.category = typeTxt.text;
            string dateFormated;
            dateFormated = activities.formattedDate.TrimStart();
            string[] stringSplit = dateFormated.Split(char.Parse(" "));
            
            dateTxt.text = stringSplit[0];
            hourTxt.text = stringSplit[1];
            ImageManager.instance.GetImage(_activities.media.url, this);

        }

        if(_conference != null)
        {
            conference = _conference;

            conference.title = nameTxt.text;
            conference.category = typeTxt.text;
            string dateFormated;
            dateFormated = conference.formattedDate.TrimStart();
            string[] stringSplit = dateFormated.Split(char.Parse(" "));

            dateTxt.text = stringSplit[0];
            hourTxt.text = stringSplit[1];
            ImageManager.instance.GetImage(conference.media.url, this);

        }

    }

    public void SetImage(Texture2D _texture)
    {
        imageTexture = _texture;
        image.sprite = Sprite.Create(_texture, new Rect(0.0f, 0.0f, imageTexture.width, imageTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)this.transform);
        image.color = Color.white;
    }
    public void SetImage(Sprite _sprite)
    {
        image.sprite = _sprite;
        image.color = Color.white;
    }
    public GameObject GetImageContainer()
    {
        return this.image.gameObject;
    }

    public void OnClick_OpenTicket()
    {
        if( conference != null )
        {
            InvokeEvent<ConferencesButtonOnClick>(new ConferencesButtonOnClick(conference));
            DOTween.KillAll();
            UIAniManager.instance.ConferencesInfoButton();
        }
        
        if(activities != null)
        {
            InvokeEvent<ActivitiesButtonOnClick>(new ActivitiesButtonOnClick(activities));
            DOTween.KillAll();
            UIAniManager.instance.ActivitiesInfoButton();
        }

    }


}
