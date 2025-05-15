using UnityEngine;

public class PISKController : MonoBehaviour
{
    public static PISKController Instance { get; private set; }

    [Header("Pressure")]
    [SerializeField] public float Pressure { get; private set; }
    [SerializeField] public float MinPressure { get; private set; } = 0f;
    [SerializeField] public float MaxPressure { get; private set; } = 100f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("There can only be one PISKController per scene!");
            return;
        }

        Instance = this;
    }

    void Update()
    {
        AdjustPressure(0.04f);
    }

    void AdjustPressure(float amount)
    {
        Pressure = Mathf.Clamp(Pressure + amount, MinPressure, MaxPressure);
    }
}
