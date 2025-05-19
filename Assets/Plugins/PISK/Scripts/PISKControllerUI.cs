// Part of the Pillo Input Simulation Kit (PISK)
// Support, Contact & Suggestions -> https://stewbyte.com

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PISKControllerUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject content;
    [SerializeField] private Slider pressureSlider;
    [SerializeField] private TextMeshProUGUI pressureValueText;
    [SerializeField] private Slider pressLerpPressureSlider;
    [SerializeField] private TextMeshProUGUI pressLerpText;
    [SerializeField] private Slider releaseLerpPressureSlider;
    [SerializeField] private TextMeshProUGUI releaseLerpText;
    [SerializeField] private Slider lerpThresholdSlider;
    [SerializeField] private TextMeshProUGUI lerpThresholdText;
    [SerializeField] private Toggle showPressureDecimalsToggle;
    [SerializeField] private TMP_Dropdown positionDropdown;
    [SerializeField] private TMP_Dropdown scaleDropdown;
    [SerializeField] private Button showHidePanelToggle;
    [SerializeField] private TextMeshProUGUI showHidePanelToggleText;
    [SerializeField] private Slider lerpCooldownSlider;
    [SerializeField] private TextMeshProUGUI lerpCooldownText;

    private RectTransform panelRect;
    private Vector2 expandedSize = new Vector2(400, 295);
    private Vector2 collapsedSize = new Vector2(45, 45);
    private bool isExpanded = true;

    void Start()
    {
        GetSliderRangeValues();
        InitializeLerpSliders();
        AddSliderListeners();
        panelRect = panel.GetComponent<RectTransform>();
        UpdatePanelPosition(positionDropdown.value);
        UpdatePanelScale(scaleDropdown.value);
        showHidePanelToggle.onClick.AddListener(TogglePanelSize);

    }

    void InitializeLerpSliders()
    {
        float pressLerp = Mathf.Round(PISKController.Instance.PressLerpSpeed * 10f) / 10f;
        float releaseLerp = Mathf.Round(PISKController.Instance.ReleaseLerpSpeed * 10f) / 10f;
        float threshold = Mathf.Round(PISKController.Instance.LerpSpeedThreshold * 1000f) / 1000f;

        pressLerpPressureSlider.value = pressLerp;
        releaseLerpPressureSlider.value = releaseLerp;
        lerpThresholdSlider.value = threshold;

        pressLerpPressureSlider.minValue = threshold;
        releaseLerpPressureSlider.minValue = threshold;
    }

    void FixedUpdate()
    {
        float pressure = PISKController.Instance.Pressure;

        pressureSlider.value = pressure;

        string format = showPressureDecimalsToggle.isOn ? "n2" : "n0";
        pressureValueText.text = $"Pressure: {pressure.ToString(format)}";

        pressLerpText.text = $"Press: {pressLerpPressureSlider.value.ToString("n2")}";
        releaseLerpText.text = $"Release: {releaseLerpPressureSlider.value.ToString("n2")}";
        lerpThresholdText.text = $"Threshold: {lerpThresholdSlider.value.ToString("n3")}";
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

        lerpThresholdSlider.onValueChanged.AddListener((value) =>
        {
            float rounded = Mathf.Round(value * 1000f) / 1000f;
            lerpThresholdText.text = $"Threshold: {rounded}";
            PISKController.Instance.SetLerpSpeedThreshold(rounded);

            pressLerpPressureSlider.minValue = rounded;
            releaseLerpPressureSlider.minValue = rounded;
        });

        lerpCooldownSlider.onValueChanged.AddListener((value) =>
        {
            float newValue = value * 10;
            PISKController.Instance.SetLerpCooldown(newValue);
            lerpCooldownText.text =  $"Cooldown: {newValue}";
        });
    }

    private void SetAnchor(RectTransform rectTransform, Vector2 anchor)
    {
        rectTransform.anchorMin = anchor;
        rectTransform.anchorMax = anchor;
        rectTransform.pivot = anchor;
        rectTransform.anchoredPosition = Vector2.zero;
    }

    public void UpdatePanelPosition(int index)
    {
        RectTransform rectTransform = panel.GetComponent<RectTransform>();

        switch (index)
        {
            case 0: // Bottom Right
                SetAnchor(rectTransform, new Vector2(1, 0));
                break;
            case 1: // Bottom Left
                SetAnchor(rectTransform, new Vector2(0, 0));
                break;
            case 2: // Top Right
                SetAnchor(rectTransform, new Vector2(1, 1));
                break;
            case 3: // Top Left
                SetAnchor(rectTransform, new Vector2(0, 1));
                break;
        }
    }

    private void SetScale(float scale)
    {
        RectTransform rectTransform = panel.GetComponent<RectTransform>();
        rectTransform.localScale = new Vector3(scale, scale, scale);
    }

    public void UpdatePanelScale(int index)
    {
        RectTransform rectTransform = panel.GetComponent<RectTransform>();

        switch (index)
        {
            case 0: // 75%
                SetScale(0.75f);
                break;
            case 1: // 100%
                SetScale(1f);
                break;
            case 2: // 150%
                SetScale(1.5f);
                break;
            case 3: // 200%
                SetScale(2f);
                break;
        }
    }

    private void TogglePanelSize()
    {
        isExpanded = !isExpanded;

        if (isExpanded)
        {
            panelRect.sizeDelta = expandedSize;
            content.SetActive(true);
            showHidePanelToggleText.text = "-";
        }
        else
        {
            panelRect.sizeDelta = collapsedSize;
            content.SetActive(false);
            showHidePanelToggleText.text = "+";
        }
    }
}