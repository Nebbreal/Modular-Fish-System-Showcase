using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PopUpController popUpScreen;
    [SerializeField] private FishingRodController fishingRodController;
    [SerializeField] private PISKController pisk;
    
    private float pressure;
    private float lastPressure;
    private float maxPressure;

    private bool isPressedIn = false;
    private InputControl _inputControl;
    
    void Start()
    {
        if (PISKController.Instance == null) return;
        maxPressure = PISKController.Instance.MaxPressure;
    }

    void Update()
    {
        UpdatePressure();
        if (isPressedIn)
        {
            SetTriggerPressure(_inputControl);
        }
        
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