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
        Pressure += 0.1f;
    }

}
