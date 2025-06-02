using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float pressureToCatch = 95f;
    [SerializeField] private float pressureToReelIn = 50f;
    [SerializeField] PopUpController popUpScreen;
    [SerializeField] private FishGenerator fishGenerator;
    [SerializeField] private Transform fishSpawnLocation;
    [SerializeField] private PISKController pisk;
    private float pressure;
    private bool readyToReelIn = false;
    private bool isPressedIn = false;
    private InputControl _inputControl;
    GameObject generatedFish;

    void Update()
    {
        UpdatePressure();
        TryCatchFish();
        if (isPressedIn)
        {
            GetInputControl(_inputControl);
        }
    }

    private void UpdatePressure()
    {
        if (PISKController.Instance == null) return;
        pressure = PISKController.Instance.Pressure;
    }

    private void GenerateFish()
    {
        if (generatedFish) Destroy(generatedFish);

        GameObject fish = fishGenerator.GenerateFish();
        fish.SetActive(true);

        generatedFish = fish;

        fish.transform.position = fishSpawnLocation.position;
        fish.transform.localScale = fishSpawnLocation.localScale;
    }

    private void TryCatchFish()
    {
        if (pressure > pressureToCatch && !readyToReelIn)
        {
            GenerateFish();
            generatedFish.gameObject.transform.SetParent(fishSpawnLocation);
            readyToReelIn = true;
        }
        else if (pressure < pressureToReelIn && readyToReelIn)
        {
            popUpScreen.gameObject.SetActive(true);
            readyToReelIn = false;
        }
    }

    public void test(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        pisk.SetPressure(value * 100);
        Debug.Log(value);
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

    private void GetInputControl(InputControl inputControl)
    {
        InputDevice inputDevice = inputControl.device;
    }
}