// Part of the Pillo Input Simulation Kit (PISK)
// Support, Contact & Suggestions → https://stewbyte.com

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
    [field: SerializeField] public float LerpSpeedThreshold { get; private set; } = 0.25f;
    [field: SerializeField] public float LerpCooldown { get; private set; } = 100f;

    private float targetPressure;
    private bool spacePressedLastFrame = false;
    private bool isCooldownActive = false;
    private float cooldownTimer = 0f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("There can only be one PISKController per scene!");
            return;
        }

        Instance = this;
    }
    


    void AdjustPressure(float amount)
    {
        Pressure = Mathf.Clamp(Pressure + amount, MinPressure, MaxPressure);
    }
    
private bool isExternalPressureSet = false;

// Update method with external pressure lerping
void Update()
{
    bool spacePressed = Keyboard.current?.spaceKey?.isPressed ?? false;

    if (spacePressedLastFrame && !spacePressed)
    {
        isCooldownActive = true;
        cooldownTimer = LerpCooldown;
    }

    if (isCooldownActive)
    {
        cooldownTimer -= Time.deltaTime * 1000f;
        if (cooldownTimer <= 0f)
        {
            isCooldownActive = false;
        }
    }

    if (isExternalPressureSet)
    {
        // External target is overriding regular input
        float lerpSpeed = (targetPressure > Pressure) ? PressLerpSpeed : ReleaseLerpSpeed;
        float rawPressure = Mathf.Lerp(Pressure, targetPressure, lerpSpeed * Time.deltaTime);
        Pressure = SmartRound(rawPressure, targetPressure);

        if (Mathf.Approximately(Pressure, targetPressure))
        {
            isExternalPressureSet = false; // Stop once target is reached
        }
    }
    else
    {
        if (spacePressed)
        {
            targetPressure = MaxPressure;
        }
        else if (!isCooldownActive)
        {
            targetPressure = MinPressure;
        }
        else
        {
            targetPressure = Pressure;
        }

        float lerpSpeed = spacePressed ? PressLerpSpeed : ReleaseLerpSpeed;
        float rawPressure = Mathf.Lerp(Pressure, targetPressure, lerpSpeed * Time.deltaTime);
        Pressure = SmartRound(rawPressure, targetPressure);
    }

    spacePressedLastFrame = spacePressed;
}

// Modified SetPressure method
public void SetPressure(float amount)
{
    targetPressure = Mathf.Clamp(amount, MinPressure, MaxPressure);
    isExternalPressureSet = true;
}

    public void SetPressLerpSpeed(float amount)
    {
        PressLerpSpeed = Mathf.Max(amount, LerpSpeedThreshold);
    }

    public void SetReleaseLerpSpeed(float amount)
    {
        ReleaseLerpSpeed = Mathf.Max(amount, LerpSpeedThreshold);
    }

    public void SetLerpSpeedThreshold(float amount)
    {
        LerpSpeedThreshold = amount;
    }

    public void SetLerpCooldown(float amount)
    {
        LerpCooldown = amount;
    }

    private float SmartRound(float value, float target)
    {
        if (Mathf.Abs(value - target) <= LerpSpeedThreshold)
        {
            return target;
        }

        return Mathf.Round(value * 100f) / 100f;
    }

}
