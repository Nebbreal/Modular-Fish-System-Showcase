using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PISKControllerUI : MonoBehaviour
{
    [SerializeField] private Slider pressureSlider;
    [SerializeField] private TextMeshProUGUI pressureValueText;

    void Start()
    {
        GetSliderRangeValues();
    }

    void FixedUpdate()
    {
        float pressure = PISKController.Instance.Pressure;

        pressureSlider.value = pressure;
        pressureValueText.text = pressure.ToString("n0");
    }

    void GetSliderRangeValues()
    {
        pressureSlider.minValue = PISKController.Instance.MinPressure;
        pressureSlider.maxValue = PISKController.Instance.MaxPressure;
    }
}
