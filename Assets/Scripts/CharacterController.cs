using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float pressureToCatch = 95f;
    [SerializeField] private float pressureToReelIn = 50f;
    [SerializeField] private float pressureToHidePopUp = 5f;
    [SerializeField] private float popUpCooldown = 1000f;
    [SerializeField] GameObject popUpScreen;
    private float pressure;
    private bool readyToReelIn = false;
    private float popUpCooldownRemaining;

    void Update()
    {
        UpdatePressure();
        TryCatchFish();
        HandleHidingPopUp();
    }

    private void UpdatePressure()
    {
        if (PISKController.Instance == null) return;
        pressure = PISKController.Instance.Pressure;
    }

    private void TryCatchFish()
    {
        if (pressure > pressureToCatch)
        {
            readyToReelIn = true;
        }
        else if (pressure < pressureToReelIn && readyToReelIn)
        {
            popUpScreen.SetActive(true);
            readyToReelIn = false;
            popUpCooldownRemaining = popUpCooldown;
        }
    }

    private void HandleHidingPopUp()
    {
        popUpCooldownRemaining -= Time.deltaTime * 1000; // seconds to milliseconds
        if (pressure > pressureToHidePopUp && popUpCooldownRemaining < 0f)
        {
            popUpScreen.SetActive(false);
        }
    }
}
