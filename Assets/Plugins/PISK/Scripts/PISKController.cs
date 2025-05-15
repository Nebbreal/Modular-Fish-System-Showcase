using UnityEngine;
using UnityEngine.InputSystem;

public class PISKController : MonoBehaviour
{
    public static PISKController Instance { get; private set; }

    [field: Header("Pressure")]
    [field: SerializeField] public float Pressure { get; private set; }
    [field: SerializeField] public float MinPressure { get; private set; } = 0f;
    [field: SerializeField] public float MaxPressure { get; private set; } = 100f;

    [Header("Pressure Lerp Settings")]
    [field: SerializeField] public float PressLerpSpeed = 4f;
    [field: SerializeField] public float ReleaseLerpSpeed = 8f;

    private float targetPressure;

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
        bool spacePressed = Keyboard.current?.spaceKey?.isPressed ?? false;

        targetPressure = spacePressed ? MaxPressure : MinPressure;
        float lerpSpeed = spacePressed ? PressLerpSpeed : ReleaseLerpSpeed;

        Pressure = Mathf.Lerp(Pressure, targetPressure, lerpSpeed * Time.deltaTime);
    }

    void AdjustPressure(float amount)
    {
        Pressure = Mathf.Clamp(Pressure + amount, MinPressure, MaxPressure);
    }

    public void SetPressLerpSpeed(float amount)
    {
        PressLerpSpeed = amount;
    }

    public void SetReleaseLerpSpeed(float amount)
    {
        ReleaseLerpSpeed = amount;
    }
}
