using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float pressureToCatch = 95f;
    [SerializeField] private float pressureToReelIn = 50f;
    [SerializeField] PopUpController popUpScreen;
    [SerializeField] private FishGenerator fishGenerator;
    [SerializeField] private Transform fishSpawnLocation;
    [SerializeField] private FishingRodController fishingRodController;

    private float pressure;
    private float lastPressure;
    private float maxPressure;
    private bool readyToReelIn = false;
    GameObject generatedFish;

    void Start()
    {
        if (PISKController.Instance == null) return;
        maxPressure = PISKController.Instance.MaxPressure;
    }

    void Update()
    {
        UpdatePressure();
        fishingRodController.RotateWithPressure(pressure, lastPressure, maxPressure);
        TryCatchFish();
    }

    private void UpdatePressure()
    {
        if (PISKController.Instance == null) return;
        lastPressure = pressure;
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
}