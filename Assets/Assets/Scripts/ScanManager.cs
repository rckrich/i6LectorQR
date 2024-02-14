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
    public TextMeshProUGUI textOut;
    public RectTransform scanzone;

    bool isCamAvailable;
    WebCamTexture cameratexture;

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
        if(isCamAvailable == false)
        {
            return;
        }
        float ratio =(float)cameratexture.width / (float)cameratexture.height;
        aspectRatioFitter.aspectRatio = ratio;

        int orientation = -cameratexture.videoRotationAngle;
        rawImageBackground.rectTransform.localEulerAngles = new Vector3 (0, 0, orientation);
    }

    private void SetupCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if(devices.Length == 0)
        {
            isCamAvailable = false;
            return;
        }
        
        foreach(WebCamDevice device in devices)
        {
            cameratexture = new WebCamTexture(device.name, (int)scanzone.rect.width, (int)scanzone.rect.height);
            break;
        }

        cameratexture.Play();
        rawImageBackground.texture = cameratexture;
        isCamAvailable = true;

    }
    
    public void OnClick_Scan()
    {
        TryScan();
    }

    private void TryScan()
    {
        try
        {
            IBarcodeReader reader = new BarcodeReader();
            Result result = reader.Decode(cameratexture.GetPixels32(), cameratexture.width, cameratexture.height);

            if(result != null)
            {
                textOut.text = result.Text;
            }
        }
        catch
        {
            textOut.text = "Failed to read";
        }
    }
    

}
