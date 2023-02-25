using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadingProgress : MonoBehaviour
{
    public static Action<bool> ProgressBarVisibility;

    private static Action<float> m_loadProgress;
    
    private static float m_loadingProgressValue;
    
    [SerializeField]
    private Image m_progressBarCounter;

    public static float LoadingProgressValue
    {
        set
        {
            m_loadingProgressValue = value;
            m_loadProgress.Invoke(m_loadingProgressValue);
        }
    }

    public static void SetProgressBarValues(int maxValue, float currentProgress) =>
        LoadingProgressValue = currentProgress / maxValue;

    private void Awake()
    {
        m_loadProgress += SetLoadingProgress;
        ProgressBarVisibility += SetProgressBarVisibility;
    }

    private void SetLoadingProgress(float value)
    {
        m_progressBarCounter.fillAmount = value;
        
        if (value < 0.9f) 
            return;
        
        LoadingProgressValue = 0;
        SetProgressBarVisibility(false);
    }

    private void SetProgressBarVisibility(bool visibility) => 
        m_progressBarCounter.transform.parent.gameObject.SetActive(visibility);
}