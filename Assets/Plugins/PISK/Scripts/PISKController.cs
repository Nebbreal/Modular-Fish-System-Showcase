using UnityEngine;
using UnityEngine.UI;

public class PISKController : MonoBehaviour
{
    static public float pressure { get; private set; }

    private Slider pressureSlider;

    void Awake()
    {
        pressureSlider = GetComponentInChildren<Slider>();
    }

    void FixedUpdate()
    {
        pressureSlider.value = pressure;
    }
}
