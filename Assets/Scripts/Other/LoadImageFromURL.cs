using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine. Networking;

public class LoadImageFromURL : MonoBehaviour
{
    public Image image;
    public string imageLink = ""; //"https://cliparting.com/wp-content/uploads/2018/03/cool-pictures-2018-3.jpg";

    private void Start()
    {
        StartCoroutine(LoadImage(imageLink));
    }

    IEnumerator LoadImage(string link)
    {
        Visuals_ImageLoading();
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(link);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Texture2D myTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Sprite newSprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f));

            Visuals_ImageFinishLoading();
            image.sprite = newSprite;
        }
    }

    void Visuals_ImageLoading()
    {
        image.color = new Color32(0, 0, 0, 0);
    }

    void Visuals_ImageFinishLoading()
    {
        image.color = new Color32(255, 255, 255, 255);

        StartCoroutine(CR_FadeColor(1f));
            
    }
    IEnumerator CR_FadeColor(float duration)
    {
        Color32 startValue = new Color32(255, 255, 255, 0);
        Color32 endValue = new Color32(255, 255, 255, 255);

        float time = 0.0f;
        while (time < duration)
        {
            image.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        image.color = endValue;
    }


}
