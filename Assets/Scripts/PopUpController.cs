using System;
using UnityEngine;

public class PopUpController : MonoBehaviour
{
    [SerializeField] private float pressureToHidePopUp = 5f;
    [SerializeField] private float popUpCooldown = 1000f;
    private float pressure;
    private float popUpCooldownRemaining;

    private void OnEnable()
    {
        popUpCooldownRemaining = popUpCooldown;
    }

    void Update()
    {
        UpdatePressure();
        HandleHidingPopUp();
    }

    private void UpdatePressure()
    {
        if (PISKController.Instance == null) return;
        pressure = PISKController.Instance.Pressure;
    }

    private void HandleHidingPopUp()
    {
        popUpCooldownRemaining -= Time.deltaTime * 1000; // seconds to milliseconds
        if (pressure > pressureToHidePopUp && popUpCooldownRemaining < 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
