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
        pressLerpPressureSlider.value = PISKController.Instance.PressLerpSpeed;
        releaseLerpPressureSlider.value = PISKController.Instance.ReleaseLerpSpeed;
        LerpThresholdSlider.value = PISKController.Instance.LerpSpeedThreshold;
    }

    void FixedUpdate()
    {
        float pressure = PISKController.Instance.Pressure;

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
            PISKController.Instance.SetPressLerpSpeed(value);
        });

        releaseLerpPressureSlider.onValueChanged.AddListener((value) =>
        {
            PISKController.Instance.SetReleaseLerpSpeed(value);
        });

        LerpThresholdSlider.onValueChanged.AddListener((value) =>
        {
            PISKController.Instance.SetLerpSpeedThreshold(value);
        });
    }
}
