using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using TMPro;
using UnityEngine.UI;


using UnityEngine.Android;



public class ScanManager : MonoBehaviour
{
    public RawImage rawImageBackground;

    public AspectRatioFitter aspectRatioFitter;
    public RectTransform scanzone;

    public float timeBetweenScans;

    public GameObject scanMessage;
    public GameObject popUp, popUpNoAuth, popUpNoInternet;
    public GameObject scanning;
    public GameObject startScan;
    public GameObject rawImage;

    public GameObject StartScan, Intro;

    bool isCamAvailable;
    WebCamTexture cameratexture;

    IEnumerator ScanCo;

    private void Update()
    {
        UpdateCameraRender();
    }

    // Start is called before the first frame update
    void Start()
    {
        ConfigViewPort();
        SetupCamera();
    }

    public void ConfigViewPort(){
        #if UNITY_IOS
            rawImage.transform.localScale = new Vector3(1, -1, 1);
        #endif
    }

    // Update is called once per frame
    void UpdateCameraRender()
    {
        if (isCamAvailable == false)
        {
            return;
        }
        
        float ratio = (float)cameratexture.width / (float)cameratexture.height;
        aspectRatioFitter.aspectRatio = ratio;

        int orientation = -cameratexture.videoRotationAngle;
        rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
    }

    private void SetupCamera()
    {
        if(isCamAvailable){
            return;
        }
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            isCamAvailable = false;
            return;
        }

        foreach (WebCamDevice device in devices)
        {
            cameratexture = new WebCamTexture(device.name, (int)scanzone.rect.width, (int)scanzone.rect.height);
            break;
        }

        cameratexture.Play();
        rawImageBackground.texture = cameratexture;
        isCamAvailable = true;

    } 

    IEnumerator ScanCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenScans);
            TryScan();
        }
    }
    
    public void OnClick_Scan()
    {
        

        if(!isCamAvailable){
            OnClick_AskAuth();
        }

        if(ScanCo == null )
        {
            cameratexture.Play();
            ScanCo = ScanCoroutine();
            StartCoroutine(ScanCo);
        }
        
    }

    private void TryScan()
    {
        
        try
        {
            IBarcodeReader reader = new BarcodeReader();
            Result result = reader.Decode(cameratexture.GetPixels32(), cameratexture.width, cameratexture.height);
            if (result != null)
            {
                if(Application.internetReachability == NetworkReachability.NotReachable){
                    cameratexture.Stop();
                    StopCoroutine(ScanCo);
                    ScanCo = null;
                    popUpNoInternet.SetActive(true);
                    startScan.SetActive(true);
                    scanning.SetActive(false);
                    
                }else{
                    StartCoroutine(Yucatani6WebCalls.CR_GetUserByPass(result.Text, OnCallBack_Initialize_User));
                    StopCoroutine(ScanCo);
                    cameratexture.Stop();
                    ScanCo = null;
                }
                
            }
        }
        catch
        {
            if(ScanCo != null){
                StopCoroutine(ScanCo);
            }
            
            cameratexture.Stop();
            ScanCo = null;
            popUp.SetActive(true);
            startScan.SetActive(true);
            scanning.SetActive(false);
        }
        
        
    }

    public void OnCallBack_Initialize_User(object[] list){
        rawImage.SetActive(false);
        if(list[0].ToString() != "200"){
            if(ScanCo != null){
                StopCoroutine(ScanCo);
            }
            cameratexture.Stop();
            ScanCo = null;
            popUp.SetActive(true);
            startScan.SetActive(true);
            scanning.SetActive(false);
            rawImage.SetActive(true);
            return;
        }
        scanMessage.SetActive(true);
        UserRoot tempuser = (UserRoot)list[1];
        scanMessage.GetComponent<ProcessScan>().InitializeScan(tempuser);
        scanning.SetActive(false);
    }

    public void StopScan()
    {
        if(ScanCo != null)
        {
            cameratexture.Stop();
            StopCoroutine(ScanCo);
            ScanCo = null;
        }
    }


    public void OnClick_ClosePopup()
    {
        popUp.SetActive(false);

    }

    private void AndroidAuth(){
        if(Permission.HasUserAuthorizedPermission(Permission.Camera)){
            StartScan.SetActive(true);
            Intro.SetActive(false);
            SetupCamera();
            
        }else{
            Permission.RequestUserPermission(Permission.Camera);
            if(Permission.HasUserAuthorizedPermission(Permission.Camera)){
                StartScan.SetActive(true);
                Intro.SetActive(false);
                SetupCamera();
                
            }else{
                popUpNoAuth.SetActive(true);
            }
            
        }
    }
    private void IOSAuth(){
        if(Application.HasUserAuthorization(UserAuthorization.WebCam)){
            StartScan.SetActive(true);
            Intro.SetActive(false);
            SetupCamera();
            
        }else{
            Application.RequestUserAuthorization(UserAuthorization.WebCam);
            if(Application.HasUserAuthorization(UserAuthorization.WebCam)){
                StartScan.SetActive(true);
                Intro.SetActive(false);
                SetupCamera();
                
            }else{
                popUpNoAuth.SetActive(true);
            }
            
        }
    }

    public void OnClick_AskAuth(){
        if(isCamAvailable){
            StartScan.SetActive(true);
            Intro.SetActive(false);
            return;
        }

        #if UNITY_IOS
            IOSAuth();
            return;
        #endif

        #if UNITY_ANDROID
            AndroidAuth();
            return;
        #endif

        SetupCamera();
    }

    

}
