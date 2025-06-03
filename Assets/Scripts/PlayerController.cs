using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PopUpController popUpScreen;
    [SerializeField] private FishingRodController fishingRodController;

    private float pressure;
    private float lastPressure;
    private float maxPressure;

    void Start()
    {
        if (PISKController.Instance == null) return;
        maxPressure = PISKController.Instance.MaxPressure;
    }

    void Update()
    {
        UpdatePressure();
        fishingRodController.RotateWithPressure(pressure, lastPressure, maxPressure);
        popUpScreen.HandleHidingPopUp(pressure);
        fishingRodController.TryCatchFish(pressure);
    }

    private void UpdatePressure()
    {
        if (PISKController.Instance == null) return;
        lastPressure = pressure;
        pressure = PISKController.Instance.Pressure;
    }
}