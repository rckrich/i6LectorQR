
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

public class ActivitityButton : AppObject, IImageDownloaderObject
{
    // Start is called before the first frame update
    public bool KeepWhenDestroyed;
    [Header("UI Object Reference")]
    public Image typeImage;
    public Image image;
    public GameObject _Image;
    private Texture2D imageTexture;
    public TextMeshProUGUI _name;
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI dateTime;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI actividadFinalizo;
    public GameObject cupoLimitado;
    public int DaysUntilEventDissappears = 7;
    //public int type;
    public string typeStr = "";
    //private string date = "";

    [Header("Type of Activity")]
    [SerializeField]
    List<Color> colors = new List<Color>();
    [SerializeField]
    List<Sprite> sprites = new List<Sprite>();

    private Activity activity;
    private string filtername;
    

    public void SetActivity(Activity _activity) { activity = _activity; }
    public void SetFiltername(string _filtername)
    {
        filtername = _filtername;
    }
    public Activity GetActivity() { return activity; }
    public string GetFiltername() { return filtername; }

    public void Initialize()
    {

        if (activity.title.Length > 27)
        {
            string _text2 = "";
            for (int k = 0; k < 27; k++)
            {

                _text2 = _text2 + activity.title[k];

            }
            _text2 = _text2 + "...";
            _name.text = _text2;
        }
        else
        {
            _name.text = activity.title;
        }
        string dateFormated;
        dateFormated = activity.formattedDate.TrimStart();
        string[] stringSplit = dateFormated.Split(char.Parse(" "));
        //CheckDate(activity.dateTime.ToString());
        dateText.text = stringSplit[0];
        dateTime.text = stringSplit[1];
        typeStr = activity.category;
        selectorString(typeStr);
        if (activity.capacity_limit != null)
            cupoLimitado.SetActive(true);
        //TODO sin limites de cupo si esta en login mode
        ImageManager.instance.GetImage(activity.media.complete_url, this);
    }
    private void Start()
    {
        AddEvents();
    }
    public void AddEvents()
    {
        AddEventListener<FilterAppObject>(FilterEvent);
        AddEventListener<NoFilterAppEvent>(NoFilterAppEvent);
    }
   public void RemoveEvent()
    {
        RemoveEventListener<FilterAppObject>(FilterEvent);
        RemoveEventListener<NoFilterAppEvent>(NoFilterAppEvent);
    }

    public void OnDestroy()
    {
        RemoveEventListener<FilterAppObject>(FilterEvent);
        RemoveEventListener<NoFilterAppEvent>(NoFilterAppEvent);
    }
   /* private void CheckDate(string date)
    {
        DateTime Actualdate = DateTime.ParseExact(date, "dd-MM-yyyy HH:mm", null);
        DateTime _dateTime = System.DateTime.UtcNow.ToLocalTime();
        //_dateTime = _dateTime.AddHours(1.50);
        DateTime TimeDateToSetAsEnded = Actualdate.AddHours(1.50);

        dateText.text = Actualdate.ToString("dd-MM");
        dateTime.text = Actualdate.ToString("HH:mm");
        actividadFinalizo.gameObject.SetActive(false);

        //if (Actualdate < _dateTime)
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
    }*/

    /*
    public void Initialize()
    {
        _name.text = activity.title;

        dateText.text = activity.date;
        typeStr = activity.category;
        selectorString(typeStr);
        Debug.Log(typeStr);
        ImageManager.instance.GetImage(activity.media.absolute_url, this);
        
        CheckDate(activity.date);  
    }

    private void CheckDate(string date){
        DateTime Actualdate = DateTime.ParseExact(date, "dd-MM-yyyy HH:mm", null);
        if (Actualdate < System.DateTime.UtcNow.ToLocalTime()){
            
            TimeSpan difference = System.DateTime.UtcNow.ToLocalTime() - Actualdate;
            if(difference.TotalDays >= DaysUntilEventDissappears){
                this.SetActive(false); 
            }else{
                Debug.Log("aqui");
                image.color = new Color32(62,62,62, 255);
            }
        }
    }
    */

    public void selectorString(string typeStrselector)
    {
        
        if (typeStrselector.Equals("ciberseguridad"))
        {
            descriptionText.text = "Ciberseguridad";
            gameObject.GetComponent<Image>().color = colors[0];
            typeImage.GetComponent<Image>().sprite = sprites[0];            
        } 
        else if(typeStrselector.Equals("inteligenciaArtificial"))
        {
            descriptionText.text = "Inteligencia Artificial";
            gameObject.GetComponent<Image>().color = colors[1];
            typeImage.GetComponent<Image>().sprite = sprites[1];
        }
        else if (typeStrselector.Equals("metaverso"))
        {
            descriptionText.text = "Metaverso";
            gameObject.GetComponent<Image>().color = colors[2];
            typeImage.GetComponent<Image>().sprite = sprites[2];
        }
        else if (typeStrselector.Equals("biotecnologia"))
        {
            descriptionText.text = "Biotecnología";
            gameObject.GetComponent<Image>().color = colors[3];
            typeImage.GetComponent<Image>().sprite = sprites[3];
        }
        else if (typeStrselector.Equals("aeroespacial"))
        {
            descriptionText.text = "Areoespacial";
            gameObject.GetComponent<Image>().color = colors[4];
            typeImage.GetComponent<Image>().sprite = sprites[4];
        }
        else if (typeStrselector.Equals("emprendimiento"))
        {
            descriptionText.text = "Emprendimiento";
            gameObject.GetComponent<Image>().color = colors[5];
            typeImage.GetComponent<Image>().sprite = sprites[5];
        }
        else if (typeStrselector.Equals("steam"))
        {
            descriptionText.text = "STEAM";
            typeImage.GetComponent<Image>().sprite = sprites[6];
        }
        else if (typeStrselector.Equals("i6_general"))
        {
            descriptionText.text = "Yucatán i6 General";
            typeImage.GetComponent<Image>().sprite = sprites[7];
        }
        else if (typeStrselector.Equals("yes"))
        {
            descriptionText.text = "YES";
            typeImage.GetComponent<Image>().sprite = sprites[8];
        }else if (typeStrselector.Equals("Espacios limitados"))
        {
            descriptionText.text = "Espacios limitados";
            //typeImage.GetComponent<Image>().sprite = sprites[9];
        }
    }
    public void selector(int tipoAct)
    {
        switch (tipoAct)
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
    public void ActivityAppObjectOnClick()
    {
        InvokeEvent <ActivitiesButtonOnClick>(new ActivitiesButtonOnClick(activity));
        DOTween.KillAll();
        UIAniManager.instance.ActivitiesInfoButton();
        
    }

    public GameObject GetImageContainer()
    {
        
        return this.image.gameObject;
    }

    public void SetImage(Sprite _sprite)
    {
        image.sprite = _sprite;
        _Image.SetActive(true);
    }

    public void SetImage(Texture2D _texture)
    {
        imageTexture = _texture;
        image.sprite = Sprite.Create(_texture, new Rect(0.0f, 0.0f, imageTexture.width, imageTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)this.transform);
        _Image.SetActive(true);
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

    public void FilterEvent(FilterAppObject _event)
    {
        filter(_event.filtername);
       
    }

    public void NoFilterAppEvent(NoFilterAppEvent _event)
    {
        if (_event.filteron)
        {
            gameObject.SetActive(true);
        }
    }
}
