using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float pressureToCatch = 95f;
    [SerializeField] private float pressureToReelIn = 50f;
    [SerializeField] PopUpController popUpScreen;
    [SerializeField] private FishGenerator fishGenerator;
    [SerializeField] private Transform fishSpawnLocation;
    private float pressure;
    private bool readyToReelIn = false;
    GameObject generatedFish;

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