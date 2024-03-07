using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using TMPro;
using UnityEngine.UI;

public class ScanManager : MonoBehaviour
{
    public RawImage rawImageBackground;

    public AspectRatioFitter aspectRatioFitter;
    public RectTransform scanzone;

    public float timeBetweenScans;

    public GameObject scanMessage;
    public GameObject popUp;
    public GameObject scanning;
    public GameObject startScan;

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
        SetupCamera();
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
        if(ScanCo == null)
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
                Debug.Log(result.Text);
                scanMessage.SetActive(true);
                scanMessage.GetComponent<ProcessScan>().Initialize(result.Text);
                scanning.SetActive(false);
                StopCoroutine(ScanCo);
                cameratexture.Stop();
                ScanCo = null;
            }
        }
        catch
        {
            StopCoroutine(ScanCo);
            cameratexture.Stop();
            ScanCo = null;
            popUp.SetActive(true);
            startScan.SetActive(true);
            scanning.SetActive(false);
        }
    }

    public void StopScan()
    {
        if(ScanCo != null)
        {
            cameratexture.Stop();
            StopCoroutine(ScanCo);
        }
    }


    public void OnClick_ClosePopup()
    {
        popUp.SetActive(false);

    }


}
