using UnityEngine;

public class FishingRodStringController : MonoBehaviour
{
    public void RotateDownward()
    {
        transform.rotation = Quaternion.Euler(Vector3.down);
    }
}
