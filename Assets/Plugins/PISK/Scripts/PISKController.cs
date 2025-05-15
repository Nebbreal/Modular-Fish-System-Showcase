using UnityEngine;

public class PISKController : MonoBehaviour
{
    static public float Pressure { get; private set; }

    void Update()
    {
        Pressure += 0.1f;
    }
}
