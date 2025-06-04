using UnityEngine;

public class FishingRodController : MonoBehaviour
{
    [SerializeField] private float maxAngle = 10f;
    [SerializeField] private FishingRodStringController fishingRodString;
    [SerializeField] private FishingRodAnimationController animationController;
    [SerializeField] private FishingRodSoundController soundController;

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
        animationController.UpdateAnimation(pressure, lastPressure);
        soundController.PlaySound(pressure, lastPressure);
    }

    private void GenerateFish()
    {
        if (generatedFish) Destroy(generatedFish);

        generatedFish = fishGenerator.GenerateFish();
        generatedFish.SetActive(true);

        generatedFish.transform.position = fishSpawnLocation.position;
        generatedFish.transform.localScale = fishSpawnLocation.localScale;
    }

    public void TryCatchFish(float pressure)
    {
        if (pressure > pressureToCatch && !readyToReelIn)
        {
            GenerateFish();
            
            Transform fishTransform = generatedFish.gameObject.transform;
            fishTransform.SetParent(fishSpawnLocation);

            readyToReelIn = true;
        }
        else if (pressure < pressureToReelIn && readyToReelIn)
        {
            popUpScreen.gameObject.SetActive(true);
            readyToReelIn = false;
        }
    }
}
