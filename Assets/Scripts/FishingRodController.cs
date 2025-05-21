using UnityEngine;

public class FishingRodController : MonoBehaviour
{
    [SerializeField] private float maxAngle = 10f;
    [SerializeField] private FishingRodStringController fishingRodString;
    float pressure;

    void Update()
    {
        UpdatePressure();
        RotateWithPressure();
    }
    
    private void UpdatePressure()
    {
        if (PISKController.Instance == null) return;
        pressure = PISKController.Instance.Pressure;
    }
    
    private void RotateWithPressure()
    {
        float maxPressure = PISKController.Instance.MaxPressure;
        float zRotation = -(pressure / maxPressure) * maxAngle;

        bool sameRotationAsOldRotation = Mathf.Approximately(transform.rotation.eulerAngles.z, zRotation);
        if (!sameRotationAsOldRotation)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, zRotation);
            fishingRodString.RotateDownward();
        }
    }
}
