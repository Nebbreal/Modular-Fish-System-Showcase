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
    private bool _readyToReelIn = false;
    private GameObject _generatedFish;
    
    public void TryCatchFish(float pressure)
    {
        if (pressure > pressureToCatch && !_readyToReelIn)
        {
            GenerateFish();
            
            Transform fishTransform = _generatedFish.gameObject.transform;
            fishTransform.SetParent(fishSpawnLocation);

            _readyToReelIn = true;
        }
        else if (pressure < pressureToReelIn && _readyToReelIn)
        {
            popUpScreen.gameObject.SetActive(true);
            _readyToReelIn = false;
        }
    }
    private void GenerateFish()
    {
        if (_generatedFish) Destroy(_generatedFish);

        _generatedFish = fishGenerator.GenerateFish();
        _generatedFish.SetActive(true);

        _generatedFish.transform.position = fishSpawnLocation.position;
    }
    
    public void RotateWithPressure(float pressure, float lastPressure, float maxPressure)
    {
        bool pressureChanged = !Mathf.Approximately(pressure, lastPressure);

        if (pressureChanged)
        {
            //Set the Z rotation based on how hard the Pillo is pressed ranging from 0% of maxAngle to 100% of maxAngle
            float zRotation = -(pressure / maxPressure) * maxAngle;
            transform.rotation = Quaternion.Euler(0f, 0f, zRotation);

            fishingRodString.RotateZ(-zRotation);
            fishingRodString.MoveDownwardWithPressure(pressure);
        }
        animationController.UpdateAnimation(pressure, lastPressure);
        soundController.PlaySound(pressure, lastPressure);
    }
}
