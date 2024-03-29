using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;


public class ReloadAppObject : AppObject
{
    private const float BASE_TOP = -25f;
    private const float BASE_BOTTOM = 0f;

    [Header("Labels")]
    private string topPullLabel = "BAJA MÁS PARA ACTUALIZAR...";
    private string topReleaseLabel = "SUELTA PARA ACTUALIZAR LA LISTA...";
    private string bottomPullLabel = "SUBE MáS PARA ACTUALIZAR...";
    private string bottomReleaseLabel = "SUELTA PARA ACTUALIZAR...";

    private Action actionToReload;
    private bool _canLoad = false;
    private bool _isLoading = false;
    private bool _visualsSetAtTheStart = false;

    [Header("Settings")]
    public bool isTopLoader = true;

    [Header("Visuals")]
    public bool visualsActive = true;
    public GameObject reloadEventObject;
    public TextMeshProUGUI reloadEventsText;
    public RectTransform Scroll;

    [Header("Pull coefficient")]
    [Range(-50f, 50f)]
    public float loadMoreObjectsLimiter = 0.15f;
    [Range(-100f, 100f)]
    public float limiterToStartReload = 0.15f;
    private void Start()
    {
        SetVisualsBeforeStartScroll();
    }

    public void SetReloadAction(Action actionToReload)
    {
        this.actionToReload = actionToReload;
    }

    public void OnValueChanged(Vector2 _vector)
    {
        //Debug.Log(Scroll.anchoredPosition.y);
        if (isTopLoader)
        {
            TopLoaderCheck(Scroll.anchoredPosition.y);
        }
        else
        {
            //BottomLoaderCheck(Scroll.anchoredPosition.y);
        }

    }

    private void TopLoaderCheck(float _vector)
    {
        if (_vector < BASE_TOP && !_isLoading && !_visualsSetAtTheStart)
        {
            SetVisualsAtStartScroll();
            _visualsSetAtTheStart = true;
        }

        if (_vector < (BASE_TOP + loadMoreObjectsLimiter))
        {
            if (!_isLoading)
            {
                PrepareReload();
            }
        }
        else
        {
            if (!_isLoading)
            {
                _canLoad = false;
                _visualsSetAtTheStart = false;
            }
        }

        if (!_canLoad) { return; }

        if ((_vector > BASE_TOP + limiterToStartReload) && _canLoad)
        {
            Reload();
        }
    }

    private void BottomLoaderCheck(float _vector)
    {
        if (_vector < BASE_BOTTOM && !_isLoading && !_visualsSetAtTheStart)
        {
            SetVisualsAtStartScroll();
            _visualsSetAtTheStart = true;
        }

        if (_vector < (BASE_BOTTOM - loadMoreObjectsLimiter))
        {
            if (!_isLoading)
            {
                PrepareReload();
            }
        }
        else
        {
            if (!_isLoading) _canLoad = false;
        }

        if (!_isLoading)
        {
            _canLoad = false;
            _visualsSetAtTheStart = false;
        }

        if ((_vector > BASE_BOTTOM - limiterToStartReload) && _canLoad)
        {
            Reload();
        }
    }

    public void SetVisualsBeforeStartScroll()
    {
        if (visualsActive && reloadEventsText != null)
        {
            reloadEventsText.gameObject.SetActive(false);
            reloadEventsText.text = isTopLoader ? topPullLabel : bottomPullLabel;
        }
        if (visualsActive && reloadEventObject != null) reloadEventObject.SetActive(false);
    }

    private void SetVisualsAtStartScroll()
    {
        if (visualsActive && reloadEventsText != null)
        {
            reloadEventsText.gameObject.SetActive(true);
            reloadEventsText.text = isTopLoader ? topPullLabel : bottomPullLabel;
        }

        if (visualsActive && reloadEventObject != null) reloadEventObject.SetActive(true);
    }

    private void PrepareReload()
    {
        if (visualsActive && reloadEventsText != null) reloadEventsText.text = isTopLoader ? topReleaseLabel : bottomReleaseLabel;

        _canLoad = true;
        _isLoading = true;
    }

    private void Reload()
    {
        SetVisualsBeforeStartScroll();

        _canLoad = false;
        _isLoading = false;
        _visualsSetAtTheStart = false;

        actionToReload();
    }


}
