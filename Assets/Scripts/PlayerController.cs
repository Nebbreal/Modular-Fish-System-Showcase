using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PopUpController popUpScreen;
    [SerializeField] private FishingRodController fishingRodController;

    private float _pressure;
    private float _lastPressure;
    private float _maxPressure;

    void Start()
    {
        if (PISKController.Instance == null) return;
        _maxPressure = PISKController.Instance.MaxPressure;
    }

    void Update()
    {
        UpdatePressure();
        fishingRodController.RotateWithPressure(_pressure, _lastPressure, _maxPressure);
        popUpScreen.HandleHidingPopUp(_pressure);
        fishingRodController.TryCatchFish(_pressure);
    }

    private void UpdatePressure()
    {
        if (PISKController.Instance == null) return;
        _lastPressure = _pressure;
        _pressure = PISKController.Instance.Pressure;
    }
}