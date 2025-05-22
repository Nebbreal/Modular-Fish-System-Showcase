using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float pressureToCatch = 95f;
    [SerializeField] private float pressureToReelIn = 50f;
    [SerializeField] PopUpController popUpScreen;
    [SerializeField] private FishGenerator fishGenerator;
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
        if(generatedFish) Destroy(generatedFish);
    
        GameObject fish = fishGenerator.GenerateFish();
        fish.SetActive(true);
        
        generatedFish = fish;
    
        fish.transform.position = Vector3.zero;
        fish.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    private void TryCatchFish()
    {
        if (pressure > pressureToCatch && !readyToReelIn)
        {
            GenerateFish();
            generatedFish.gameObject.transform.SetParent(popUpScreen.gameObject.transform);
            readyToReelIn = true;
        }
        else if (pressure < pressureToReelIn && readyToReelIn)
        {
            popUpScreen.gameObject.SetActive(true);
            readyToReelIn = false;
        }
    }
}