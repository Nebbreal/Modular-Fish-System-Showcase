using Hulan.PilloSDK.DeviceManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PopUpController popUpScreen;
    [SerializeField] private FishingRod fishingRod;
    [SerializeField] private bool controllerMode;
    
    private float _pressure;
    private float _lastPressure;
    private float _maxPressure;

    private bool _isPressedIn = false;
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
        ControllerMode();
        
        if (PiskExists()) 
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

    #region PilloControls
    private void HandlePressureChange(string identifier, int pressure)
        {
            //pressure is taken from the PilloDeviceManager. 
            
            //Amplify the pressure due to the system assuming different values than the Pillo's values
            //Our system the pressure range of (0-100)
            float newPressure = pressure / _maxPressure * 100f * 4f;
            _pressure = Mathf.Clamp(newPressure, 0f, 100f);
        }
    #endregion

    #region ControllerControls
    private void ControllerMode()
    {
        if (!controllerMode) return;
        
        if (!PiskExists())
        {
            Debug.LogWarning("PISK is required for controller support to be properly implemented");
        }
        else if (controllerMode && _isPressedIn)
        {
            SetTriggerPressure(_inputControl);
        }
        else if (controllerMode && PiskExists())
        {
            PISKController.Instance.SetPressure(0);
        }
    }
    
    public void ToggleTriggerPress(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _inputControl = context.control;
            _isPressedIn = true;
        }
        else if (context.canceled)
        {
            _isPressedIn = false;
        }
    }

    private void SetTriggerPressure(InputControl inputControl)
    {
        if (!PiskExists()) return;
        
        InputDevice inputDevice = inputControl.device;
        if (inputDevice is Gamepad)
        {
            float triggerValue = Gamepad.current.rightTrigger.ReadValue();
            PISKController.Instance.SetPressure(triggerValue * 100);
        }
    }
    #endregion

    #region Utility
    private static bool PiskExists()
    {
        if (PISKController.Instance && PISKController.Instance.isActiveAndEnabled) return true;
        
        
        return false;
    }
    #endregion
}