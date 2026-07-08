using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSceneCanvas : MonoBehaviour
{
    [SerializeField] GameObject misWindow;
    [SerializeField] GameObject successWindow;
    [SerializeField] TextMeshProUGUI tekauValue;
    [SerializeField] GameObject nextStageButton;
    [SerializeField] GameObject inGameRetryButton;
    [SerializeField] bool disableNextStage = false;

    public void ShowMisWindow()
    {
        misWindow.SetActive(true);
        inGameRetryButton.SetActive(false);
    }

    public void ShowSuccessWindow(bool showNextStage)
    {
        if (disableNextStage) showNextStage = false;
        nextStageButton.SetActive(showNextStage);
        successWindow.SetActive(true);
    }

    public void OnPushRetryButton()
    {
        Debug.Log("OnPushRetryButton");
        GameSceneController.instance.Retry();
    }

    public void SetTekazu(int value)
    {
        tekauValue.text = value.ToString();
    }
}
