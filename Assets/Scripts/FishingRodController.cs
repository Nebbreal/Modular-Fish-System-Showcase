using UnityEngine;

public class FishingRodController : MonoBehaviour
{
    [SerializeField] private float maxAngle = 10f;
    [SerializeField] private FishingRodStringController fishingRodString;
    [SerializeField] private FishingRodAnimationController fishingRodAnimationController;

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
}
