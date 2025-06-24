using Hulan.PilloSDK.DeviceManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PopUpController popUpScreen;
    [SerializeField] private FishingRod fishingRod;

    private float _pressure;
    private float _lastPressure;
    private float _maxPressure;

    private bool isPressedIn = false;
    private InputControl _inputControl;
    
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
        if (isPressedIn)
        {
            SetTriggerPressure(_inputControl);
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
    
    public void ToggleTriggerPress(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _inputControl = context.control;
            isPressedIn = true;
        }
        else if (context.canceled)
        {
            isPressedIn = false;
        }
    }

    private void SetTriggerPressure(InputControl inputControl)
    {
        InputDevice inputDevice = inputControl.device;
        if (inputDevice is Gamepad)
        {
            float triggerValue = Gamepad.current.rightTrigger.ReadValue();
            pisk.SetPressure(triggerValue * 100);
        }
    }
}