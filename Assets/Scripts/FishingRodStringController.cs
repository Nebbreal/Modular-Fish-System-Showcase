using UnityEngine;

public class FishingRodStringController : MonoBehaviour
{
    [SerializeField] private float stringLength = 250f;
    private Vector3 anchorOffset;

    private void Start()
    {
        anchorOffset = transform.position - transform.parent.position;
    }

    public void RotateDownward()
    {
        transform.rotation = Quaternion.Euler(Vector3.down);
    }

    public void MoveDownwardWithPressure(float pressure)
    {
        float loweringDistance = pressure / 100f * stringLength;
        Vector3 transformVector = Vector3.down * loweringDistance;

        Vector3 anchoredPosition = transform.parent.position + anchorOffset;
        Vector3 worldPosition = anchoredPosition + transformVector;
        transform.position = worldPosition;
    }
}