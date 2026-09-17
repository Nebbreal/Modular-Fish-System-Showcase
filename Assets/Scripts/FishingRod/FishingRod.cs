using System;
using ModularFishSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class FishingRod : MonoBehaviour
{
    [SerializeField] private float maxAngle = 10f;

    [SerializeField, Tooltip("The maximum amount the fishing rod moves down")]
    private float maxDownwardMovement;
    private float _startingYPositionRod;
    
    [SerializeField] private FishingRodString fishingRodString;
    [SerializeField] private FishingRodAnimation rodAnimation;
    [SerializeField] private FishingRodSound sound;

    [SerializeField] PopUpController popUpScreen;
    [SerializeField] private Transform fishSpawnLocation;

    [SerializeField] private FishGenerator fishGenerator;
    [SerializeField] private float pressureToCatch = 95f;
    [SerializeField] private float pressureToReelIn = 50f;
    private bool _readyToReelIn = false;
    private GameObject _generatedFish;
    
    private void Start()
    {
        _startingYPositionRod = transform.position.y;
    }

    public void TryCatchFish(float pressure)
    {
        if (pressure > pressureToCatch && !_readyToReelIn)
        {
            GenerateFish();
            
            fishingRodString.AttachFish(_generatedFish);
            
            _readyToReelIn = true;
        }
        else if (pressure < pressureToReelIn && _readyToReelIn)
        {
            fishingRodString.DetachFish(_generatedFish);
            Transform fishTransform = _generatedFish.gameObject.transform;
            
            fishTransform.SetParent(fishSpawnLocation);
            fishTransform.localScale = new(1f, 1f, 1f);
            fishTransform.localPosition = Vector3.zero;
            
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
    
    public void RotateWithPressure(float pressure, float lastPressure)
    {
        bool pressureChanged = !Mathf.Approximately(pressure, lastPressure);

        if (pressureChanged)
        {
            float pressurePercentage = pressure / 100f;
            
            //Set the Z rotation based on how hard the Pillo is pressed ranging from 0% of maxAngle to 100% of maxAngle
            float zRotation = -pressurePercentage * maxAngle;
            transform.rotation = Quaternion.Euler(0f, 0f, zRotation);
    
            //Set the Y position to adjust for the rotation
            float newYPositon = _startingYPositionRod - maxDownwardMovement * pressurePercentage;
            Vector3 newRodPosition = new Vector3(transform.position.x, newYPositon);
            transform.position = newRodPosition;
            
            fishingRodString.RotateZ(-zRotation);
        }
        float yOffset = fishingRodString.GetHoverYOffset(_readyToReelIn);
        fishingRodString.MoveDownwardWithPressure(pressure, yOffset);
        rodAnimation.UpdateAnimation(pressure, lastPressure);
        sound.PlaySound(pressure, lastPressure);
    }
}
