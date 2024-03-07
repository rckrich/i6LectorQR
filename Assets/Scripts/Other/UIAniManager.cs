using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIAniManager : Manager
{
    public GameObject MainCanvas,Intro, Activities, Conferences, Map, ActivitiesInfo, ConferencesInfo, Header, PopUp, Error; 
    //private bool HasMapMoved = false;

    private static UIAniManager _instance;
    private Vector2 FinalPosition;
    private Vector2 RestPositionDown;
    private Vector2 RestPositionSide;


    public static UIAniManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIAniManager>();
            }
            return _instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetPosition();

    }

    void SetPosition(){
        FinalPosition = MainCanvas.transform.position;
        RestPositionDown = new Vector2(MainCanvas.transform.position.x, -MainCanvas.transform.position.y);
        RestPositionSide = new Vector2(MainCanvas.transform.position.x*4, MainCanvas.transform.position.y);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   /* public void IntroButton(){
        SetPosition();
        Activities.transform.DOMove(FinalPosition, 0.5F, false).OnComplete(() => {Intro.SetActive(false);});
        Header.transform.DOMove(FinalPosition, 0.5F, false);

    }*/

    public void ConferencesInfoButton(){
        ConferencesInfo.SetActive(true);
        SetPosition();
        //ConferencesInfo.transform.DOMove(FinalPosition, 0.5F, false).OnComplete(() => {Conferences.SetActive(false);});
        ConferencesInfo.transform.DOMove(FinalPosition, 0.5F, false);
    }
    public void ConferencesInfoBackButton(){
        SetPosition();
       ConferencesInfo.transform.DOMove(RestPositionSide, 0.5F, false).OnComplete(() => {ConferencesInfo.SetActive(false);});
        
    }


    public void ActivitiesInfoButton(){
        SetPosition();
        ActivitiesInfo.SetActive(true);
        // ActivitiesInfo.transform.DOMove(FinalPosition, 0.5F, false).OnComplete(() => {Activities.SetActive(false);});
        ActivitiesInfo.transform.DOMove(FinalPosition, 0.5F, false);

    }
    public void ActivitiesInfoBackButton(){
        SetPosition();
        ActivitiesInfo.transform.DOMove(RestPositionSide, 0.5F, false).OnComplete(() => {ActivitiesInfo.SetActive(false);});
        
    }

    public void MapButton(){
        Map.SetActive(true);
        SetPosition();
        //HasMapMoved = true;
        Map.transform.DOMove(FinalPosition, 0.5F, false);
        Activities.transform.DOMove(RestPositionDown, 0.5F, false).OnComplete(() => {Activities.SetActive(false);});
        Conferences.transform.DOMove(RestPositionDown, 0.5F, false).OnComplete(() => {Conferences.SetActive(false);});
    }
    
    /*public void ConferencesButton(){
        SetPosition();
        if(HasMapMoved){
            Map.transform.DOMove(RestPositionDown, 0.5F, false).OnComplete(() => {Map.SetActive(false);});
            HasMapMoved = false;
        }
        Activities.transform.DOMove(RestPositionDown, 0.5F, false).OnComplete(() => {Activities.SetActive(false);});
        Conferences.transform.DOMove(FinalPosition, 0.5F, false);
    }*/

   /* public void ActivitiesButton(){
        SetPosition();
        if(HasMapMoved){
            Map.transform.DOMove(RestPositionDown, 0.5F, false).OnComplete(() => {Map.SetActive(false);});
            HasMapMoved = false;
        }
        Conferences.transform.DOMove(RestPositionDown, 0.5F, false).OnComplete(() => {Conferences.SetActive(false);});
        Activities.transform.DOMove(FinalPosition, 0.5F, false);

    }*/

    public void PopUpOpen(){
        PopUp.SetActive(true);
        SetPosition();
        PopUp.transform.DOMove(FinalPosition, 0.5F, false);
    
        
        
    }
    public void PopUpClose(){
        SetPosition();
        PopUp.transform.DOMove(RestPositionDown, 0.5F, false).OnComplete(() => {PopUp.SetActive(false); PopUp.transform.position = FinalPosition;});
        
    }

    public void ErrorOpen(){
        Error.SetActive(true);
        SetPosition();
        Error.transform.DOMove(FinalPosition, 0.5F, false);
    }
    public void errorClose(){
        SetPosition();
        Error.transform.DOMove(RestPositionDown, 0.5F, false).OnComplete(() => {Error.SetActive(false);});
    }

    public void SideTransitionExit(GameObject GA){
        SetPosition();
        GA.transform.DOMove(RestPositionSide, 0.5F, false).OnComplete(() => {GA.SetActive(false);});
    }
    public void SideTransitionEnter(GameObject GA){
        SetPosition();
        GA.transform.position = RestPositionSide;
        GA.SetActive(true);
        GA.transform.DOMove(FinalPosition, 0.5F, false);
    }



}
