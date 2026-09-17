using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace ModularFishSystem.Demo.Configuration
{
    public class ConfigurationUI : MonoBehaviour
    {
        [SerializeField]
        private FishGenerator fishGenerator;
    
        //Technically generalizing more and storing these in an array would be more scalable, but it's overkill for the current scope
        [SerializeField] 
        private Slider fishSizeSlider;
        [SerializeField] 
        private Slider shiftableBaseChanceSlider;
        [SerializeField] 
        private Slider shiftablePartChanceSlider;
        [SerializeField] 
        private Slider hatChanceSlider;
        [SerializeField] 
        private Slider minimumValueSlider;
        [SerializeField] 
        private Slider minimumSaturationSlider;

        public void OnEnable()
        {
            //Sync sliders with the actual settings
            fishSizeSlider.value = fishGenerator.fishSize;
            shiftableBaseChanceSlider.value = fishGenerator.shiftableBaseChance;
            shiftablePartChanceSlider.value = fishGenerator.shiftablePartChance;
            hatChanceSlider.value = fishGenerator.hatChance;
            minimumValueSlider.value = fishGenerator.minimumValue;
            minimumSaturationSlider.value = fishGenerator.minimumSaturation;
            
            UpdateVisualPercentageText(fishSizeSlider);
            UpdateVisualPercentageText(shiftableBaseChanceSlider);
            UpdateVisualPercentageText(shiftablePartChanceSlider);
            UpdateVisualPercentageText(hatChanceSlider);
            UpdateVisualPercentageText(minimumValueSlider);
            UpdateVisualPercentageText(minimumSaturationSlider);
            
            fishSizeSlider.onValueChanged.AddListener(OnFishSizeChanged);
            shiftableBaseChanceSlider.onValueChanged.AddListener(OnShiftableBaseChanceChanged);
            shiftablePartChanceSlider.onValueChanged.AddListener(OnShiftablePartChanceChanged);
            hatChanceSlider.onValueChanged.AddListener(OnHatChanceChanged);
            minimumValueSlider.onValueChanged.AddListener(OnMinimumValueChanged);
            minimumSaturationSlider.onValueChanged.AddListener(OnMinimumSaturationChanged);
        }

        public void OnDisable()
        {
            fishSizeSlider.onValueChanged.RemoveListener(OnFishSizeChanged);
            shiftableBaseChanceSlider.onValueChanged.RemoveListener(OnShiftableBaseChanceChanged);
            shiftablePartChanceSlider.onValueChanged.RemoveListener(OnShiftablePartChanceChanged);
            hatChanceSlider.onValueChanged.RemoveListener(OnHatChanceChanged);
            minimumValueSlider.onValueChanged.RemoveListener(OnMinimumValueChanged);
            minimumSaturationSlider.onValueChanged.RemoveListener(OnMinimumSaturationChanged);
        }
        
        private void UpdateVisualPercentageText(Slider slider)
        {
            TMP_Text text = slider.GetComponentInChildren<SliderPercentageText>().gameObject.GetComponent<TMP_Text>();
            float percentage = slider.value / (slider.maxValue - slider.minValue) * 100;
            int visualPercentage = (int)Mathf.Round(percentage);
            text.text = $"{visualPercentage}%";
        }

        #region EventHandlers
        private void OnFishSizeChanged(float value)
        {
            fishGenerator.fishSize = value;
            UpdateVisualPercentageText(fishSizeSlider);
        }

        private void OnShiftableBaseChanceChanged(float value)
        {
            fishGenerator.shiftableBaseChance = value;
            UpdateVisualPercentageText(shiftableBaseChanceSlider);
        }

        private void OnShiftablePartChanceChanged(float value)
        {
            fishGenerator.shiftablePartChance = value;
            UpdateVisualPercentageText(shiftablePartChanceSlider);
        }

        private void OnHatChanceChanged(float value)
        {
            fishGenerator.hatChance = value;
            UpdateVisualPercentageText(hatChanceSlider);
        }

        private void OnMinimumValueChanged(float value)
        {
            fishGenerator.minimumValue = value;
            UpdateVisualPercentageText(minimumValueSlider);
        }

        private void OnMinimumSaturationChanged(float value)
        {
            fishGenerator.minimumSaturation = value;
            UpdateVisualPercentageText(minimumSaturationSlider);
        }
        #endregion
    }
}
