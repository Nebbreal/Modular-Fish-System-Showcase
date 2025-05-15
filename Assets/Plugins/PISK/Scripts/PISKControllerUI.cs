using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PISKControllerUI : MonoBehaviour
{
    [SerializeField] private Slider pressureSlider;
    [SerializeField] private TextMeshProUGUI pressureValueText;
    [SerializeField] private Slider pressLerpPressureSlider;
    [SerializeField] private TextMeshProUGUI pressLerpText;
    [SerializeField] private Slider releaseLerpPressureSlider;
    [SerializeField] private TextMeshProUGUI releaseLerpText;
    [SerializeField] private Slider LerpThresholdSlider;
    [SerializeField] private TextMeshProUGUI LerpThresholdText;

    void Start()
    {
        GetSliderRangeValues();
        InitializeLerpSliders();
        AddSliderListeners();
    }

    void InitializeLerpSliders()
    {
        pressLerpPressureSlider.value = Mathf.Round(PISKController.Instance.PressLerpSpeed * 10f) / 10f;
        releaseLerpPressureSlider.value = Mathf.Round(PISKController.Instance.ReleaseLerpSpeed * 10f) / 10f;
        LerpThresholdSlider.value = Mathf.Round(PISKController.Instance.LerpSpeedThreshold * 1000f) / 1000f;
    }

    void FixedUpdate()
    {
        float pressure = Mathf.Round(PISKController.Instance.Pressure);

        pressureSlider.value = pressure;

        pressureValueText.text = $"Pressure: {pressure.ToString("n1")}";
        pressLerpText.text = $"Press: {pressLerpPressureSlider.value.ToString("n2")}";
        releaseLerpText.text = $"Release: {releaseLerpPressureSlider.value.ToString("n2")}";
        LerpThresholdText.text = $"Threshold: {LerpThresholdSlider.value.ToString("n3")}";
    }

    void GetSliderRangeValues()
    {
        pressureSlider.minValue = PISKController.Instance.MinPressure;
        pressureSlider.maxValue = PISKController.Instance.MaxPressure;
    }

    void AddSliderListeners()
    {
        pressLerpPressureSlider.onValueChanged.AddListener((value) =>
        {
            float rounded = Mathf.Round(value * 10f) / 10f;
            PISKController.Instance.SetPressLerpSpeed(rounded);
        });

        releaseLerpPressureSlider.onValueChanged.AddListener((value) =>
        {
            float rounded = Mathf.Round(value * 10f) / 10f;
            PISKController.Instance.SetReleaseLerpSpeed(rounded);
        });

        LerpThresholdSlider.onValueChanged.AddListener((value) =>
        {
            float rounded = Mathf.Round(value * 1000f) / 1000f;
            LerpThresholdText.text = $"Threshold: {rounded}";
            PISKController.Instance.SetLerpSpeedThreshold(rounded);
        });
    }
}