// Part of the Pillo Input Simulation Kit (PISK)
// This is an example script and can be safely removed or modified.
// Support, Contact & Suggestions → https://stewbyte.com

using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    float pressure;
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        UpdatePressure();           // Get the latest pressure value from PISK
        ScaleWithPressure();        // Visually scale the object based on pressure
        ChangeColorWithPressure();  // Change color based on pressure zones
        RotateWithPressure();       // Rotate object when pressure is within a specific range
    }

    private void UpdatePressure()
    {
        // Always check if the singleton is initialized before using it
        if (PISKController.Instance == null) return;

        // Get the current simulated pressure value
        pressure = PISKController.Instance.Pressure;
    }

    private void ScaleWithPressure()
    {
        // Map pressure (0 to MaxPressure) to scale range (1 to 5)
        float scale = Mathf.Lerp(1f, 5f, pressure / PISKController.Instance.MaxPressure);

        // Apply uniform scale based on simulated pressure
        transform.localScale = Vector3.one * scale;
    }

    private void ChangeColorWithPressure()
    {
        Color targetColor;

        // This uses percentages of MaxPressure, so it's flexible to any pressure range.
        // You can still do simple comparisons like "pressure > 50" if preferred.
        if (pressure <= PISKController.Instance.MaxPressure * 0.33f)
        {
            targetColor = Color.red; // Low pressure zone
        }
        else if (pressure <= PISKController.Instance.MaxPressure * 0.66f)
        {
            targetColor = new Color(1f, 0.5f, 0f); // Orange - medium pressure
        }
        else
        {
            targetColor = Color.green; // High pressure
        }

        // Set the material color based on pressure zone
        objectRenderer.material.color = targetColor;
    }

    private void RotateWithPressure()
    {
        float maxPressure = PISKController.Instance.MaxPressure;

        float third = maxPressure * 0.33f;
        float twoThirds = maxPressure * 0.66f;

        float zRotation = 0f;

        // Rotation is inactive below 33% pressure
        // Between 33–66%, rotation increases from 0° to 45°
        // Above 66%, rotation is capped at 45°
        if (pressure >= third && pressure <= twoThirds)
        {
            float normalized = (pressure - third) / (twoThirds - third); // 0 to 1
            zRotation = 45f * normalized;
        }
        else if (pressure > twoThirds)
        {
            zRotation = 45f;
        }

        // Apply rotation around Z axis only
        transform.rotation = Quaternion.Euler(0f, 0f, zRotation);
    }
}
