using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadingProgress : MonoBehaviour
{
    [SerializeField]
    private Image ProgressBarCounter;

    private static float m_loadingProgressValue;

    private static Action<float> LoadProgress;
    public static Action<bool> ProgressBarVisibility;
    
    public static float LoadingProgressValue
    {
        set
        {
            if (m_loadingProgressValue != value)
            {
                m_loadingProgressValue = value;
                LoadProgress.Invoke(m_loadingProgressValue);
            }
        }
    }

    public static void SetProgressBarValues(int maxValue, float currentProgress) =>
        LoadingProgressValue = currentProgress / maxValue;

    private void Awake()
    {
        LoadProgress += SetLoadingProgress;
        ProgressBarVisibility += SetProgressBarVisibility;
    }

    private void SetLoadingProgress(float value)
    {
        ProgressBarCounter.fillAmount = value;
        if (value == 1.0f)
        {
            LoadingProgressValue = 0;
            SetProgressBarVisibility(false);
        }
    }

    private void SetProgressBarVisibility(bool visibility) => 
        ProgressBarCounter.transform.parent.gameObject.SetActive(visibility);
}