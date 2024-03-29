using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpViewModel : ViewModel
{
    [Header("Message Only Pop Up - Texts to change")]
    public TMP_Text messageOnlyPopUpTitleText;
    public TMP_Text messageOnlyPopUpBodyText;
    public TMP_Text messageOnlyPopUpActionButtonText;
    public RectTransform messageOnlyObjectToRebuildLayout;
    [Header("Option Choice Pop Up - Texts to change")]
    public TMP_Text optionChoicePopUpTitleText;
    public TMP_Text optionChoicePopUpBodyText;
    public TMP_Text optionChoicePopUpActionButtonText;
    public RectTransform optionChoiceObjectToRebuildLayout;
    [Header("Pop Ups Game Object Reference")]
    public GameObject messageOnlyPopUp;
    public GameObject OptionChoicePopUp;

    private Action action;
    private PopUpViewModelTypes type;

    private void OnEnable()
    {
        StartRebuildLayout();
    }

    public override void Initialize(params object[] list)
    {
        type = (PopUpViewModelTypes)list[0];

        if (type == PopUpViewModelTypes.MessageOnly)
        {
            if(messageOnlyPopUpTitleText != null)
                messageOnlyPopUpTitleText.text = (string)list[1];
            if(messageOnlyPopUpBodyText != null)
                messageOnlyPopUpBodyText.text = (string)list[2];
            if (messageOnlyPopUpActionButtonText != null)
                messageOnlyPopUpActionButtonText.text = (string)list[3];
            if (messageOnlyPopUp != null)
                messageOnlyPopUp.SetActive(true);
            if(OptionChoicePopUp != null)
                OptionChoicePopUp.SetActive(false);
            return;
        }

        if (type == PopUpViewModelTypes.OptionChoice)
        {
            if(optionChoicePopUpTitleText != null)
                optionChoicePopUpTitleText.text = (string)list[1];
            if(optionChoicePopUpBodyText != null)
                optionChoicePopUpBodyText.text = (string)list[2];
            if(optionChoicePopUpActionButtonText != null)
                optionChoicePopUpActionButtonText.text = (string)list[3];
            if(OptionChoicePopUp != null)
                OptionChoicePopUp.SetActive(true);
            if (messageOnlyPopUp != null)
                messageOnlyPopUp.SetActive(false);
            return;
        }

    }

    public void SetPopUpAction(System.Action action)
    {
        this.action = action;
    }

    public void ActionButtonOnClick()
    {
        action();
    }

    public void ExitButtonOnClick()
    {
        NewScreenManager.instance.BackToPreviousView();
    }

    private void StartRebuildLayout()
    {
        StartCoroutine(CR_RebuildLayout());
    }

    private IEnumerator CR_RebuildLayout()
    {
        yield return new WaitForSeconds(0.01f);
        LayoutRebuilder.ForceRebuildLayoutImmediate((messageOnlyObjectToRebuildLayout));
        LayoutRebuilder.ForceRebuildLayoutImmediate((optionChoiceObjectToRebuildLayout));
    }
}
