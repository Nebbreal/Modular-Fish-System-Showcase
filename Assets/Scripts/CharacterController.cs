using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float pressureToCatch = 95f;
    [SerializeField] private float pressureToReelIn = 50f;
    [SerializeField] PopUpController popUpScreen;
    private float pressure;
    private bool readyToReelIn = false;

    void Update()
    {
        UpdatePressure();
        TryCatchFish();
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
            popUpScreen.gameObject.SetActive(true);
            readyToReelIn = false;
        }
    }
}
