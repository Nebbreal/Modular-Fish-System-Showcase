using UnityEngine;

public class FishingRodController : MonoBehaviour
{
    [SerializeField] private float maxAngle = 10f;
    [SerializeField] private FishingRodStringController fishingRodString;
    [SerializeField] private FishingRodAnimationController fishingRodAnimationController;

    [SerializeField] PopUpController popUpScreen;
    [SerializeField] private FishGenerator fishGenerator;
    [SerializeField] private Transform fishSpawnLocation;
    [SerializeField] private float pressureToCatch = 95f;
    [SerializeField] private float pressureToReelIn = 50f;
    private bool readyToReelIn = false;
    GameObject generatedFish;


    public void RotateWithPressure(float pressure, float lastPressure, float maxPressure)
    {
        bool pressureChanged = !Mathf.Approximately(pressure, lastPressure);

        if (pressureChanged)
        {
            float zRotation = -(pressure / maxPressure) * maxAngle;
            transform.rotation = Quaternion.Euler(0f, 0f, zRotation);

            fishingRodString.RotateDownward();
            fishingRodString.MoveDownwardWithPressure(pressure);
        }
        fishingRodAnimationController.UpdateAnimation(pressure, lastPressure);
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

    public void TryCatchFish(float pressure)
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
