using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PopUpController popUpScreen;
    [FormerlySerializedAs("fishingRodController")] [SerializeField] private FishingRod fishingRod;

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
        fishingRod.RotateWithPressure(_pressure, _lastPressure, _maxPressure);
        popUpScreen.HandleHidingPopUp(_pressure);
        fishingRod.TryCatchFish(_pressure);
    }

    private void UpdatePressure()
    {
        if (PISKController.Instance == null) return;
        _lastPressure = _pressure;
        _pressure = PISKController.Instance.Pressure;
    }
}