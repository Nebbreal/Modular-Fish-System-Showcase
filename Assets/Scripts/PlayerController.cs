using Hulan.PilloSDK.DeviceManager;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PopUpController popUpScreen;
    [SerializeField] private FishingRod fishingRod;

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
        
        fishingRod.RotateWithPressure(_pressure, _lastPressure);
        popUpScreen.HandleHidingPopUp(_pressure);
        fishingRod.TryCatchFish(_pressure);
    }

    private void LateUpdate()
    {
        _lastPressure = _pressure;
    }

    private void HandlePressureChange(string identifier, int pressure)
    {
        //pressure is from the PilloDeviceManager. It needs to scale to our pressure range (0-100)
        _pressure = pressure / _maxPressure * 100f;
    }
}