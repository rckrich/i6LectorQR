using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;
using System.Net;

public class ImageManager : Manager
{
    private static ImageManager self;

    public static ImageManager instance
    {
        get
        {
            if (self == null)
            {
                self = (ImageManager)GameObject.FindObjectOfType(typeof(ImageManager));
            }
            return self;
        }
    }

    private const string GET_IMAGE_URI = "";

    [Header("Error at loading image texture")]
    public Sprite errorSprite;

    public void GetImage(string imageURL, IImageDownloaderObject ImageAppObject)
    {
        StartCoroutine(getProductImage(imageURL, ImageAppObject));
    }

    private IEnumerator getProductImage(string imageURL, IImageDownloaderObject ImageAppObject)
    {
        GameObject imageContainer = ImageAppObject.GetImageContainer();
        
        if(imageURL == null)
        {
            Debug.Log("ImageUrl_Null");
            if (imageContainer != null)
                ImageAppObject.SetImage(errorSprite);

            yield break;
        }

        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(imageURL))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(webRequest.error);
                if (imageContainer != null)
                    ImageAppObject.SetImage(errorSprite);
            }
            else
            {
                Texture2D retrievedTexture2D = DownloadHandlerTexture.GetContent(webRequest);
                if (imageContainer != null)
                    ImageAppObject.SetImage(retrievedTexture2D);
            }
        }
    }
}