using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using System.Linq;
using static log4net.Appender.RollingFileAppender;

public class ConferenceButton : AppObject, IImageDownloaderObject
{
    [Header("UI Object Reference")]
    public Image typeImage;
    public Image image;
    public GameObject _image;
    private Texture2D imageTexture;
    public TextMeshProUGUI _name;
    public TextMeshProUGUI dateText, dateTime;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI typeOfEvent;
    public TextMeshProUGUI actividadFinalizo;

    public int DaysUntilEventDissappears = 7;
    //public int type;
    public string typeStr = "";
    //private string date = "";
    public bool KeepWhenDestroyed;

    [Header("Type of Conference")]
    [SerializeField]
    List<Color> colors = new List<Color>();
    [SerializeField]
    List<Sprite> sprites = new List<Sprite>();

    private Conference conferences;
    private string filtername;
    //private bool filteron = false;
    public void SetConferences(Conference _conferences) { conferences = _conferences; }
    public void SetFiltername(string _filtername)
    {
        filtername = _filtername;
    }
    public Conference GetConferences() { return conferences; }
    public string GetFiltername() { return filtername; }

    // Start is called before the first frame update
    void Start()
    {
        selectorString(typeStr);
        AddEvents();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddEvents()
    {
        AddEventListener<ConferencesFilterAppEvent>(FilterEvent);
        AddEventListener<ConferencesNoFilterAppEvent>(NoFilterAppEvent);
    }

    void OnDestroy()
    {
        RemoveEvent();
    }

    public void RemoveEvent()
    {
        RemoveEventListener<ConferencesFilterAppEvent>(FilterEvent);
        RemoveEventListener<ConferencesNoFilterAppEvent>(NoFilterAppEvent);
    }

    public void Initialize()
    {
        if (conferences.title.Length > 27)
        {
            string _text2 = "";
            for (int k = 0; k < 27; k++)
            {

                _text2 = _text2 + conferences.title[k];

            }
            _text2 = _text2 + "...";
            _name.text = _text2;
        }
        else
        {
            _name.text = conferences.title;
        }

        string[] newdate = conferences.formattedDate.Split("/");
        string dateformatted = "";
        for (int i = 0; i < newdate.Length; i++)
        {
            dateformatted += newdate[i];
            if( i == 1)
            {
                dateformatted += "<br>";
            }
        }

        string dateFormated;
        dateFormated = conferences.formattedDate.TrimStart();
        string[] stringSplit = dateFormated.Split(char.Parse(" "));
        //CheckDate(activity.dateTime.ToString());
        dateText.text = stringSplit[0];
        dateTime.text = stringSplit[1];

        //typeOfEvent.text = conferences.category;
        typeStr = conferences.category;
        selectorString(typeStr);
        ImageManager.instance.GetImage(conferences.media.complete_url, this);
        

    }
    private void CheckDate(string date)
    {
        DateTime Actualdate = DateTime.ParseExact(date, "dd-MM-yyyy HH:mm", null);
        DateTime _dateTime = System.DateTime.UtcNow.ToLocalTime();
        //_dateTime = _dateTime.AddHours(1.50);
        DateTime TimeDateToSetAsEnded = Actualdate.AddHours(1.50);

        dateText.text = Actualdate.ToString("dd-MM");       
        timeText.text = Actualdate.ToString("HH:mm");
        actividadFinalizo.gameObject.SetActive(false);

        //if (Actualdate < dateTime)
        if (TimeDateToSetAsEnded < _dateTime)
        {    
            TimeSpan difference = System.DateTime.UtcNow.ToLocalTime() - Actualdate;
            //Debug.Log("Date Time: " + _dateTime);
            //Debug.Log("Now Time: " + System.DateTime.UtcNow.ToLocalTime());
            //Debug.Log("Actual Date: " + Actualdate);
            //Debug.Log("TimeDateToSetAsEnded: " + TimeDateToSetAsEnded);
            //Debug.Log("Difference: " + difference);

            if (difference.TotalDays >= DaysUntilEventDissappears)
            {
                this.SetActive(false);
                RemoveEvent();
            }
            else
            {
                //Debug.Log("aqui");
                image.color = new Color32(62, 62, 62, 255);
                actividadFinalizo.gameObject.SetActive(true);
            }
        }
    }





    public void selectorString(string typeStrselector)
    {
        if (typeStrselector.Equals("ciberseguridad"))
        {
            typeOfEvent.text = "Ciberseguridad";
            gameObject.GetComponent<Image>().color = colors[0];
            typeImage.GetComponent<Image>().sprite = sprites[0];
        }
        else if (typeStrselector.Equals("inteligenciaArtificial"))
        {
            typeOfEvent.text = "Inteligencia Artificial";
            gameObject.GetComponent<Image>().color = colors[1];
            typeImage.GetComponent<Image>().sprite = sprites[1];
        }
        else if (typeStrselector.Equals("metaverso"))
        {
            typeOfEvent.text = "Metaverso";
            gameObject.GetComponent<Image>().color = colors[2];
            typeImage.GetComponent<Image>().sprite = sprites[2];
        }
        else if (typeStrselector.Equals("biotecnologia"))
        {
            typeOfEvent.text = "Biotecnología";
            gameObject.GetComponent<Image>().color = colors[3];
            typeImage.GetComponent<Image>().sprite = sprites[3];
        }
        else if (typeStrselector.Equals("aeroespacial"))
        {
            typeOfEvent.text = "Areoespacial";
            gameObject.GetComponent<Image>().color = colors[4];
            typeImage.GetComponent<Image>().sprite = sprites[4];
        }
        else if (typeStrselector.Equals("emprendimiento"))
        {
            typeOfEvent.text = "Emprendimiento";
            gameObject.GetComponent<Image>().color = colors[5];
            typeImage.GetComponent<Image>().sprite = sprites[5];
        }
        else if (typeStrselector.Equals("steam"))
        {
            typeOfEvent.text = "STEAM";
            typeImage.GetComponent<Image>().sprite = sprites[6];
        }
        else if (typeStrselector.Equals("i6_general"))
        {
            typeOfEvent.text = "Yucatán i6 General";
            typeImage.GetComponent<Image>().sprite = sprites[7];
        }
        else if (typeStrselector.Equals("yes"))
        {
            typeOfEvent.text = "YES";
            typeImage.GetComponent<Image>().sprite = sprites[8];
        }
    }
    public void selector(int tipoConf)
    {
        switch (tipoConf)
        {
            case 1:
                gameObject.GetComponent<Image>().color = colors[0];
                typeImage.GetComponent<Image>().sprite = sprites[0];
                break;
            case 2:
                gameObject.GetComponent<Image>().color = colors[1];
                typeImage.GetComponent<Image>().sprite = sprites[1];
                break;
            case 3:
                gameObject.GetComponent<Image>().color = colors[2];
                typeImage.GetComponent<Image>().sprite = sprites[2];
                break;
            case 4:
                gameObject.GetComponent<Image>().color = colors[3];
                typeImage.GetComponent<Image>().sprite = sprites[3];
                break;
            case 5:
                gameObject.GetComponent<Image>().color = colors[4];
                typeImage.GetComponent<Image>().sprite = sprites[4];
                break;
            case 6:
                gameObject.GetComponent<Image>().color = colors[5];
                typeImage.GetComponent<Image>().sprite = sprites[5];
                break;
            case 7:
                gameObject.GetComponent<Image>().color = colors[6];
                typeImage.GetComponent<Image>().sprite = sprites[6];
                break;
            case 8:
                gameObject.GetComponent<Image>().color = colors[7];
                typeImage.GetComponent<Image>().sprite = sprites[7];
                break;
            case 9:
                gameObject.GetComponent<Image>().color = colors[8];
                typeImage.GetComponent<Image>().sprite = sprites[8];
                break;
        }
    }

    public GameObject GetImageContainer()
    {
        return this.image.gameObject;
    }


    public void SetImage(Sprite _sprite)
    {
        image.sprite = _sprite;
        _image.SetActive(true);
    }

    public void SetImage(Texture2D _texture)
    {
        imageTexture = _texture;
        image.sprite = Sprite.Create(_texture, new Rect(0.0f, 0.0f, imageTexture.width, imageTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)this.transform);
        _image.SetActive(true);
    }

    public void ConferenceButtonOnClick()
    {
        InvokeEvent<ConferencesButtonOnClick>(new ConferencesButtonOnClick(conferences));
        DOTween.KillAll();
        UIAniManager.instance.ConferencesInfoButton();
    }

    public void filter(string _type)
    {
        if (!_type.Equals(typeStr))
        {
            gameObject.SetActive(false);

        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void FilterEvent(ConferencesFilterAppEvent _event)
    {
        filter(_event.filtername);

    }

    public void NoFilterAppEvent(ConferencesNoFilterAppEvent _event)
    {
        if (_event.filteron)
        {
            gameObject.SetActive(true);
        }
    }
}
