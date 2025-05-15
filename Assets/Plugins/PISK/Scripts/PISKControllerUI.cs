using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PISKControllerUI : MonoBehaviour
{
    [SerializeField] private Slider pressureSlider;
    [SerializeField] private TextMeshProUGUI pressureValueText;

    void FixedUpdate()
    {
        float pressure = PISKController.Instance.Pressure;

        pressureSlider.value = pressure;
        pressureValueText.text = pressure.ToString("n0");
    }

    void SetSliderMinMaxValues(float min = 0f, float max = 100f)
    {
        pressureSlider.minValue = min;
        pressureSlider.maxValue = max;
    }
}
