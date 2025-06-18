using Hulan.PilloSDK.DeviceManager;
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
        if (PISKController.Instance != null)
        {
            _maxPressure = PISKController.Instance.MaxPressure; 
        }
        else
        {
            PilloDeviceManager.onPeripheralPressureDidChange += HandlePressureChange;
            _maxPressure = 1024f;
        }
    }

    void Update()
    {
        if (PISKController.Instance != null)
        {
            _pressure = PISKController.Instance.Pressure;
        }
        
        fishingRodController.RotateWithPressure(_pressure, _lastPressure, _maxPressure);
        popUpScreen.HandleHidingPopUp(_pressure);
        fishingRodController.TryCatchFish(_pressure);
    }

    private void LateUpdate()
    {
        _lastPressure = _pressure;
    }

    private void HandlePressureChange(string identifier, int pressure)
    {
        _pressure = pressure / _maxPressure * 100f; //to convert to the pressure values (0-100)
    }
}